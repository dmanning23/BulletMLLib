# BulletMLLib TODO

Known problems found during the September 2026 code review, with suggested fixes. Items marked **(verified)** were reproduced with a test. Everything else comes from reading the code.

Test suite status at the time of review: **13 of 272 tests fail** (`dotnet test`). Several of the items below explain those failures. Getting the suite green is a priority, because until it passes CI can't catch regressions.

---

## 1. XML parsing (`BulletMLLib/Nodes`, `BulletPattern.cs`)

### 1.1 Self-referencing bullets and actions crash with a stack overflow **(verified) — FIXED**

> **Status:** Fixed. `FireTask` no longer builds the fired bullet's task tree, so bullets can fire themselves. `ActionRefNode.ValidateNode` rejects circular `actionRef` chains with an `InvalidDataException`. Covered by `Tests/RecursionTest.cs`.

**Problem:** A bullet that fires itself is a common BulletML idiom for splitting or chained bullets, but it crashes the process:

```xml
<bullet label="split">
  <action>
    <wait>30</wait>
    <fire><bulletRef label="split"/></fire>
    <vanish/>
  </action>
</bullet>
```

`FireTask.ParseChildNode` ([FireTask.cs](../BulletMLLib/Tasks/FireTask.cs)) builds the referenced bullet's full task tree up front. That tree contains the same `bulletRef`, so it recurses forever. A self-referencing `actionRef` does the same thing through `ActionTask.ParseTasks`. .NET can't catch a stack overflow, so one bad pattern file kills the whole game.

**Fix:**
- Stop building the referenced bullet's task tree inside `FireTask`. `FireTask` only needs the bullet's `<speed>` and `<direction>` nodes, and the fired bullet builds its own tree in `Bullet.InitNode`. With that change, recursive bullets work, which is the correct BulletML behavior.
- For `actionRef`, detect cycles in `ValidateNode` by keeping a stack of the refs being resolved, and throw a clear error, or build child action tasks lazily.

### 1.2 Refs with no `label` resolve to the wrong node **(verified)**

**Problem:** `ActionRefNode`, `BulletRefNode` and `FireRefNode` call `FindLabelNode(Label, …)` even when `Label` is null. `null == null` matches the first *unlabeled* node of that type, so `<actionRef/>` quietly links to an unrelated action.

**Fix:** In each ref node's `ValidateNode`, throw if `string.IsNullOrEmpty(Label)`. `FindLabelNode` should also never match on a null label.

### 1.3 `<fire>` with no `<bullet>`/`<bulletRef>` throws `NullReferenceException` **(verified)**

**Problem:** `FireNode.ValidateNode` ([FireNode.cs](../BulletMLLib/Nodes/FireNode.cs)) calls `refNode.FindMyBulletNode()` without checking whether `refNode` is null. The user gets a bare NRE with no hint of which node is broken.

**Fix:** Check for null and throw a descriptive error, e.g. *"fire node 'label' has no bullet or bulletRef child"*.

### 1.4 The DTD is never enforced

**Problem:** `BulletPattern.ParseXML` sets `ValidationType.None`, so `MyValidationEventHandler` never fires. Missing required labels, missing bullets and bad `type` values all reach the node code unchecked. The content-pipeline path and the `NETFX_CORE` path don't process the DTD at all.

**Fix:** Do structural validation in code (`ValidateNode`) so it runs the same way on every load path. Items 1.2, 1.3, 1.5 and 1.7 cover most of what matters. Either delete the dead validation handler or switch on `ValidationType.DTD` for the file path as an extra check.

### 1.5 Default `type` handling is inconsistent

**Problem:** `DirectionNode` defaults to `aim` and turns invalid values into `aim`. `SpeedNode`, `HorizontalNode` and `VerticalNode` keep `NodeType.none`, and each task treats `none` as absolute through its own `default:` branch. The parser also accepts types that make no sense, such as `<speed type="aim">`. The failing `SpeedNodeDefaultValue` test is caused by this.

**Fix:** Give `SpeedNode`, `HorizontalNode` and `VerticalNode` an `absolute` default and override `NodeType` the same way `DirectionNode` does. Throw on a type that isn't valid for the node.

### 1.6 Parse errors don't say where they happened

**Problem:**
- `StringToName` and `StringToType` use `Enum.Parse`. A typo like `<chnageSpeed>` or `type="Relative"` produces *"Requested value … was not found"* with no line number or parent element.
- A CDATA section (`#cdata-section`) produces the same error.
- Missing refs throw `NullReferenceException`, which looks like a library bug rather than a problem with the pattern file.

**Fix:**
- Add a `BulletMLException` that carries the element name, label and, when available, the line number (`IXmlLineInfo`).
- Use `Enum.TryParse` and throw that exception on failure.
- Treat CDATA as text.

### 1.7 Duplicate labels are silently ambiguous

**Problem:** `FindLabelNode` searches the whole tree and returns the first match. When two nodes of the same type share a label, which one wins depends on where it sits in the document.

**Fix:** During validation, build a label table per node type, `Dictionary<(NodeName, string), BulletMLNode>`. Throw on duplicates and resolve refs from the table, which also makes lookups O(1).

### 1.8 Minor node issues

- **Every node allocates an equation.** Each `BulletMLNode` constructs a `BulletMLEquation` and registers `rank`, `rand` and every callback, including `<action>`, `<bullet>` and `<repeat>`, which never hold a value. Create the equation lazily, only when the node has text.
- **Stray text is accepted silently.** Text inside structural nodes (`<action>oops<wait>…`) is parsed as an equation and ignored. Throw instead.
- **`BulletMLNode.Id` is unused.** Remove it.
- **`FireRefNode.ValidateNode` never validates its children.** It skips `base.ValidateNode()`, so its `<param>` children are never validated. Validate the children explicitly.
- **`FireNode.BulletDescriptionNode` has a public setter.** Make it `private set`.
- **`ActionNode.FindParentRepeatNode` throws `NullReferenceException`.** It should throw an invalid-data error instead.

---

## 2. Runtime behavior (`BulletMLLib/Tasks`, `Bullet.cs`)

### 2.1 Parameters don't reach a fired bullet's actions **(verified)**

**Problem:** In `FireTask.Run`, the new bullet's top task gets its `Owner` set to the `FireTask`, but the `<param>` values live on `BulletRefTask`. `Owner` is also set *after* `InitNode` has already run `InitTask`, so values computed at setup see no parameters at all. For example, `<bulletRef label="b"><param>15</param></bulletRef>` into a bullet with `<changeSpeed><speed type="relative">$1</speed>…` never adds 15. Only the bullet's own `<speed>`/`<direction>` read the parameter correctly, which is why `CorrectSpeedFromParam` passes.

**Fix:** Pass the parameter owner into `InitNode`, e.g. `InitNode(node, BulletMLTask paramOwner)`. Set it on the top task *before* `ParseTasks` and `InitTask`, and use `BulletRefTask` (or the `FireTask` for inline `<bullet>`) as that owner.

### 2.2 `TimeSpeed` is applied inconsistently **(verified)**

**Problem:**
- `Bullet.Update` adds `Acceleration` to velocity without scaling by `TimeSpeed`, so slowdown doesn't slow horizontal or vertical drift. A bullet with `TimeSpeed = 0` still moves.
- `AccelTask`, `ChangeSpeedTask` (relative/sequence) and `ChangeDirectionTask` (relative/sequence/aim) count their term down by `TimeSpeed` but add the full step every frame. At `TimeSpeed = 0.5` they overshoot by 2×.

**Fix:** Scale every per-frame delta by `bullet.TimeSpeed`, and scale `Acceleration` in `Bullet.Update`:

```csharp
Vector2 vel = (Acceleration + Direction.ToVector2() * Speed) * TimeSpeed * Scale;
```

### 2.3 `ChangeSpeedTask` never resets `RunDelta`

**Problem:** `ChangeDirectionTask.SetupTask` resets `RunDelta = 0`, but `ChangeSpeedTask.SetupTask` doesn't. On the second pass of a repeat, the absolute branch divides by `Duration - RunDelta = 0`, so speed becomes Infinity or NaN.

**Fix:** Add `RunDelta = 0;` to `ChangeSpeedTask.SetupTask`, plus a test that repeats a `changeSpeed` twice.

### 2.4 Aim `changeDirection` never reaches its target

**Problem:** The aim (and default) branch of `ChangeDirectionTask.GetDirection` divides the remaining angle by the full `Duration`, not by the time left. With term 2 and a target at 90°, the bullet ends at 67°. The `ChangeDirectionAim1` test fails because of this.

**Fix:** Divide by `Duration - RunDelta`, as the absolute branch already does.

### 2.5 `VanishTask` never sets `TaskFinished`

**Problem:** After a vanish, the task can run again, calling `RemoveBullet` repeatedly, and the tasks after it keep running in the same frame, so a `<fire>` after `<vanish/>` still fires.

**Fix:** Set `TaskFinished = true` and return `RunStatus.Stop`, or add a `Vanished` flag on `Bullet` that `Update` checks, so nothing runs after a vanish.

### 2.6 `UpdateAsync` isn't thread-safe

**Problem:** Nodes and their equations are shared between bullets. `FireTask` and `VanishTask` call `CreateBullet`/`RemoveBullet` on the manager from worker threads. Updating bullets in parallel will race.

**Fix:** Remove `UpdateAsync`, or document it as unsafe and require managers to be thread-safe. The work per bullet is too small for parallelism to pay off anyway.

### 2.7 Pooled bullets keep state from their previous use

**Problem:** `Bullet.InitNode` clears `Tasks` but doesn't reset `Acceleration`. A manager that reuses bullet objects will carry the previous bullet's acceleration into the new one.

**Fix:** Reset `Acceleration = Vector2.Zero` in `InitNode`.

### 2.8 Minor runtime issues

- **`InitTopNode` only finds `top1` to `top9`.** Scan for every action whose label starts with `top` instead.
- **`GetParamValue(0)` indexes `ParamList[-1]`.** Treat `$0` as invalid, or return 0.
- **A negative `<term>` passes the divide-by-zero guard** (`0.0f == Duration`). Use `Duration <= 0`.
- **`<repeat><times>` is evaluated once, when the bullet is parsed** (`ActionTask.ParseTasks`), so `$rand` or `$rank` in `times` is never re-rolled. Evaluate it in `SetupTask`.

---

## 3. Documentation and API accuracy

- **`FireData.cs`**: never used anywhere, and its docs (default speed 1) contradict `FireTask`, which falls back to the parent bullet's speed. **Delete the class.**
- **`PatternType` / `BulletPattern.Orientation`**: the orientation is stored but never read, and the enum docs claim horizontal and vertical change the direction math. Either implement it (rotate by 90° for horizontal patterns) or document it as informational only.
- **`NodeType.sequence` doc**: it says the value is "added to the previous value each time the node fires". That's true for `<fire>` direction and speed. In `changeSpeed`, `changeDirection` and `accel` it's actually added **every frame**. Document both meanings.
- **Task constructor XML docs**: `AccelTask`, `ChangeSpeedTask`, `ChangeDirectionTask`, `VanishTask`, `SetSpeedTask`, `SetDirectionTask`, `RepeatTask` and `WaitTask` all say they initialize `BulletMLTask`. Point each at its own class.
- **`BulletMLEquation.cs`**: the constructor's doc comment and signature are indented one tab too few.

---

## 4. Tests and housekeeping

- **Get the suite green.** The 13 failing tests are:
  - `SpeedNodeDefaultValue` (1.5)
  - `ChangeDirectionAim1`, `ChangeDirectionRel`, `ChangeDirectionRel1` (2.4)
  - `OneAction1`
  - `FireDirectionInitCorrect` 0–3
  - `FireAimDirection`
  - `CorrectDirection`
  - `IgnoreSequenceInitSpeed`
  - `InitDirectionWithSequence`

  For each one, decide whether the test or the code is wrong. For example, `OneAction1` expects an `ActionTask` at the top, but `InitNode` wraps the top node in a `BulletMLTask`.
- **Add regression tests** for every **(verified)** item above before fixing it.
- **`.gitignore`**:
  - It ignores all of `.claude/`. Only `.claude/settings.local.json` is machine-local, so narrow the rule so shared settings or commands can be committed.
  - Add the missing final newline.

---

## Suggested order

1. Parsing safety: 1.1, 1.2, 1.3. A bad pattern file shouldn't be able to crash the game.
2. Runtime correctness: 2.1, 2.3, 2.4, 2.2.
3. Go through the failing tests and get the suite green (section 4).
4. Validation and error-message improvements: 1.4 to 1.7.
5. Documentation cleanup (section 3) and the minor items.
