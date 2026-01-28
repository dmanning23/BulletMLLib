# Architecture

Overview of BulletMLLib's internal design, covering the parsing pipeline, runtime execution model, and key design patterns.

## High-Level Flow

```
XML File                    Parse Phase              Runtime Phase
─────────                   ───────────              ─────────────
                            ┌───────────┐
bulletml.xml  ──ParseXML──> │ Node Tree │ ──InitNode──> Task Tree ──Update()──> Position
                            └───────────┘               per bullet     each frame
                            (shared, reusable)          (per instance)
```

1. **Parse phase**: A BulletML XML file is parsed once into a tree of `BulletMLNode` objects stored in a `BulletPattern`.
2. **Runtime phase**: When a bullet is initialized, the shared node tree is used to create a per-bullet tree of `BulletMLTask` objects. Each frame, `Update()` runs the tasks and updates the bullet's position.

## Parsing Pipeline

### 1. XML Loading (`BulletPattern.ParseXML`)

```
XML file → XmlReader → XmlDocument → BulletMLNode.Parse()
```

- Opens the XML file using `XmlReader` with DTD validation.
- Alternatively loads via MonoGame's `ContentManager` for content pipeline integration.
- Reads the `type` attribute from `<bulletml>` to determine pattern orientation.

### 2. Node Tree Construction (`BulletMLNode.Parse`)

Each XML element maps to a `BulletMLNode` (or a specialized subclass). The parse process:

1. Read the element name and map it to a `NodeName` enum value.
2. Create the appropriate node type via `NodeFactory.CreateNode()`.
3. Read attributes (`type`, `label`) and store on the node.
4. Parse text content as a mathematical equation via `BulletMLEquation`.
5. Recursively parse child elements into child nodes.
6. Set parent-child relationships.

### 3. Validation (`BulletMLNode.ValidateNode`)

After parsing, the tree is validated:

- Reference nodes (`bulletRef`, `actionRef`, `fireRef`) resolve their `label` to the target node.
- `ActionNode` objects find their parent `RepeatNode` (if any).
- Missing references throw descriptive exceptions.

### Node Type Hierarchy

```
BulletMLNode (base)
├── ActionNode          ← <action>
├── ActionRefNode       ← <actionRef>
├── BulletNode          ← <bullet>
├── BulletRefNode       ← <bulletRef>
├── FireNode            ← <fire>
├── FireRefNode         ← <fireRef>
├── DirectionNode       ← <direction>
├── SpeedNode           ← <speed>
├── ChangeDirectionNode ← <changeDirection>
├── ChangeSpeedNode     ← <changeSpeed>
├── AccelNode           ← <accel>
├── WaitNode            ← <wait>
├── RepeatNode          ← <repeat>
├── VanishNode          ← <vanish>
├── TermNode            ← <term>
├── TimesNode           ← <times>
├── ParamNode           ← <param>
├── HorizontalNode      ← <horizontal>
└── VerticalNode        ← <vertical>
```

All nodes are created by `NodeFactory.CreateNode()` based on the `NodeName` enum.

## Runtime Execution

### Task Tree Creation (`Bullet.InitNode`)

When a bullet is initialized from a node:

1. A root `BulletMLTask` is created from the given node.
2. `ParseTasks()` recursively builds the task tree from the node tree.
3. `InitTask()` calls `SetupTask()` on each task to prepare initial state.
4. The task tree is stored in the bullet's `Tasks` list.

### Task Type Hierarchy

```
BulletMLTask (base)
├── ActionTask          ← Repeats child tasks N times
├── FireTask            ← Creates a new bullet
├── SetDirectionTask    ← Sets initial direction during fire setup
├── SetSpeedTask        ← Sets initial speed during fire setup
├── ChangeDirectionTask ← Gradually changes direction
├── ChangeSpeedTask     ← Gradually changes speed
├── AccelTask           ← Applies acceleration components
├── WaitTask            ← Pauses for N frames
├── RepeatTask          ← Resets sequence nodes on repeat
└── VanishTask          ← Removes the bullet
```

### Per-Frame Execution (`Bullet.Update`)

Each frame:

1. Each task in `Tasks` calls `Run(bullet)`.
2. `ActionTask.Run()` iterates through child tasks sequentially. When a child returns `Stop` (e.g., `WaitTask` still counting), execution pauses until next frame. When a child returns `End`, it moves to the next child.
3. After all tasks run, position is updated:

```
velocity = (Acceleration + Direction.ToVector2() * Speed * TimeSpeed) * Scale
X += velocity.X
Y += velocity.Y
```

### Task Execution Details

**ActionTask**: Manages sequential execution of child tasks. Tracks a repeat counter. When all children complete, decrements the counter and restarts if repeats remain.

**FireTask**:
1. During `SetupTask()`, evaluates direction and speed from child `SetDirectionTask` / `SetSpeedTask`.
2. During `Run()`, calls `IBulletManager.CreateBullet()`, sets position to the parent bullet's position, sets direction and speed, then calls `InitNode()` on the new bullet with the bullet description node.
3. Handles `sequence` type by accumulating direction/speed across firings.

**WaitTask**: Stores a frame counter. Returns `Stop` until the counter reaches zero, then returns `End`.

**ChangeDirectionTask**: Calculates the per-frame direction delta needed to reach the target direction over the specified term. Each frame, adds the delta to the bullet's direction.

**ChangeSpeedTask**: Same approach as `ChangeDirectionTask` but for speed.

**AccelTask**: Calculates per-frame horizontal and vertical acceleration deltas. Applies them to the bullet's `Acceleration` vector each frame.

**VanishTask**: Calls `IBulletManager.RemoveBullet()` immediately.

**RepeatTask**: When its parent `ActionTask` repeats, resets all `sequence`-type nodes so they accumulate correctly on the next iteration.

## Expression Evaluation

Node values (direction, speed, wait, times, etc.) are not simple numbers -- they are mathematical expressions parsed by the `BulletMLEquation` class (extending the Equationator library).

Supported features:
- Arithmetic: `+`, `-`, `*`, `/`
- Parentheses: `(1 + 2) * 3`
- Built-in variables: `$rank` (difficulty), `$rand` (random 0-1)
- Parameters: `$1`, `$2`, etc. (from `<param>` in references)
- Custom callbacks: any key in `IBulletManager.CallbackFunctions`

Expressions are parsed once during node tree construction and evaluated at runtime when the task executes.

## Design Patterns

### Factory Pattern
`NodeFactory.CreateNode()` maps `NodeName` enum values to the correct `BulletMLNode` subclass. This keeps node creation centralized and extensible.

### Composite Pattern
Both the node tree and task tree use the composite pattern. Nodes contain child nodes; tasks contain child tasks. This allows recursive traversal and execution.

### Template Method
`BulletMLTask.Run()` provides a base execution framework. Subclasses override specific behavior (e.g., `FireTask` creates bullets, `WaitTask` counts frames) while sharing the common `Run()` interface.

### Flyweight Pattern
The parsed node tree is shared across all bullets using the same pattern. Each bullet creates its own lightweight task tree that references the shared nodes. This avoids re-parsing XML for every bullet.

### Separation of Definition and Execution
Nodes are the "what" (static definition from XML). Tasks are the "how" (runtime state machine). This separation allows one pattern definition to drive thousands of concurrent bullets, each with independent execution state.

## Threading

`Bullet.UpdateAsync()` runs the `Update()` method on a background thread via `Task.Factory.StartNew()`. This can be used when many bullets need updating, but care must be taken with thread safety in `IBulletManager` callbacks (particularly `CreateBullet`, `RemoveBullet`, and `PlayerPosition`).

## Coordinate System

- **Origin**: upper-left corner of the screen.
- **X**: increases to the right.
- **Y**: increases downward.
- **Direction**: measured in radians.
  - In `vertical` patterns: 0 = up, PI/2 = right, PI = down.
  - In `horizontal` patterns: 0 = right, PI/2 = down, PI = left.
- **Speed**: pixels per frame.
- Directions are wrapped via `MathHelper.WrapAngle` to stay within [-PI, PI].

## Dependencies

```
BulletMLLib
├── MonoGame.Framework.DesktopGL 3.8
│   └── Vector2, MathHelper, Random, ContentManager
├── Equationator 5.x
│   └── Mathematical expression parsing (BulletMLEquation extends Equation)
└── Vector2Extensions 5.x
    └── Angle(), ToVector2() extension methods
```

MonoGame is referenced with `PrivateAssets=All`, meaning it is not transitively passed to consumers of the NuGet package.
