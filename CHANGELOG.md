# Changelog

## Unreleased

### Changed

- **Duplicate labels are rejected.** Two `<action>`, `<bullet>` or `<fire>` nodes with the same label used to load, and a ref silently used whichever came first in the file. `ParseXML` now throws, pointing at the second one:

  ```
  Duplicate <action> label "shoot", first used on line 7 (line 10, column 3)
  ```

  Labels only need to be unique within a type, so an `<action label="a">` and a `<bullet label="a">` can still coexist. **A pattern that relied on the first-match behavior will now fail to load.**

## 5.1.1

### Improved

- **Load errors say where the problem is.** Every error now includes the line and column in the XML file, and `ParseXML` throws a `BulletMLException` whose message contains the problem itself, not just the file name:

  ```
  Error reading "Content/boss.xml": <changeSpeed> requires a <term> child (line 12, column 4)
  ```

  `BulletMLException` has `FileName`, `LineNumber` and `LinePosition` properties. It derives from `Exception`, and its `InnerException` is still the `InvalidDataException` described in 5.1, so existing `catch` code keeps working. Malformed XML reports its location the same way, with an `XmlException` as the inner exception.
- **Unknown elements are reported clearly.** A typo like `<chnageSpeed>` now gives `Unknown element <chnageSpeed>` with its location, instead of an `ArgumentException` from `Enum.Parse`.
- **CDATA sections are read as text,** so `<speed><![CDATA[1 + 2]]></speed>` works.
- **Missing reference targets throw `InvalidDataException`** (inside `BulletMLException`) with a location, instead of `NullReferenceException`. For example: `Couldn't find the action node "nope" (line 6, column 4)`.
- **An invalid `type` on the root `<bulletml>` element** is reported like any other invalid type, instead of throwing an `ArgumentException`.

## 5.1.0

This release makes pattern loading much stricter and fixes several ways a bad pattern file could crash a game. **Some patterns that loaded in 5.0 will now throw when you call `ParseXML`.** See [Breaking changes](#breaking-changes) before upgrading.

### Fixed

- **Bullets that fire themselves no longer crash the game.** A `<bulletRef>` pointing back to the bullet that contains it caused a stack overflow, which .NET can't catch. This common pattern for splitting or chained bullets now works:

  ```xml
  <bullet label="split">
    <action>
      <wait>30</wait>
      <fire><bulletRef label="split"/></fire>
      <vanish/>
    </action>
  </bullet>
  ```

- **Circular `actionRef` chains are reported instead of crashing.** An action that references itself, directly or through other actions, used to overflow the stack. It now fails to load with a "circular actionRef" error. An action that fires a bullet which runs that same action is still allowed.
- **Refs with no `label` are rejected.** An `<actionRef/>`, `<bulletRef/>` or `<fireRef/>` without a label used to link silently to the first unlabeled node of that type.
- **A `<fire>` with no `<bullet>` or `<bulletRef>`** now reports which fire node is broken, instead of throwing a bare `NullReferenceException`.
- **`<speed>`, `<horizontal>` and `<vertical>` now default to `absolute`,** as the BulletML spec says. They used to report `NodeType.none`. Runtime behavior is unchanged, because the tasks already treated `none` as absolute.

### Breaking changes

**Patterns are validated against the BulletML DTD rules.** The DTD was never actually enforced before, so malformed patterns loaded and behaved unpredictably. `ParseXML` now throws for:

| Problem | Example error |
|---|---|
| An element in a place the DTD doesn't allow | `<speed> is not allowed inside <action>` |
| A required child is missing | `<changeSpeed> requires a <term> child` |
| A `<repeat>` with nothing to repeat | `<repeat> requires an <action> or <actionRef> child` |
| A `type` the element doesn't accept | `"aim" is not a valid type for a <speed> node` |
| A misspelled or wrongly-cased `type` | `"Absolute" is not a valid type for a <direction> node` |
| A `type` on an element that doesn't take one | `"relative" is not a valid type for a <wait> node` |
| A ref with no `label` | `An actionRef node is missing a label` |
| A circular `actionRef` | `The action node "loop" has a circular actionRef` |

The required children are:
- `<changeSpeed>`: `<speed>` and `<term>`
- `<changeDirection>`: `<direction>` and `<term>`
- `<accel>`: `<term>`
- `<repeat>`: `<times>` plus at least one `<action>` or `<actionRef>`
- `<fire>`: `<bullet>` or `<bulletRef>`

**Other behavior changes:**

- **`<direction>` with an invalid `type` now throws.** It used to be silently treated as `aim`.
- **`BulletPattern.MyValidationEventHandler` has been removed.** It was public but never called, because DTD validation was turned off.

**Upgrading:** load every pattern your game ships with once, for example in a unit test that calls `ParseXML` on each file. Anything that breaks the rules fails straight away with a message naming the bad element.

### Catching load errors

Validation errors are thrown as `System.IO.InvalidDataException`, wrapped in an `Exception` whose message names the file:

```csharp
try
{
    pattern.ParseXML("Content/boss.xml");
}
catch (Exception ex) when (ex.InnerException is InvalidDataException)
{
    // ex.Message:                "Error reading "Content/boss.xml""
    // ex.InnerException.Message: "<changeSpeed> requires a <term> child"
}
```

Missing or mistyped reference targets (a `label` that points at nothing) still throw `NullReferenceException`, as in 5.0.

### Documentation

- New `docs/` folder with a getting-started guide, a BulletML guide, an architecture overview and an API reference.
- Expanded README with a quick-start guide and an example pattern.
- XML doc comments cleaned up throughout the public API.
