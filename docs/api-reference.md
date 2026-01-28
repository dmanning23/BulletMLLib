# API Reference

Complete reference for all public types in the `BulletMLLib` namespace.

## Classes

### Bullet

```csharp
public abstract class Bullet : IBullet
```

Base class for all bullets. Inherit from this class and override the abstract members to create your game's bullet type.

**Constructor:**

```csharp
protected Bullet(IBulletManager myBulletManager)
```

| Parameter | Description |
|---|---|
| `myBulletManager` | The bullet manager that owns this bullet. Must not be null. |

**Abstract Members (must override):**

| Member | Type | Description |
|---|---|---|
| `X` | `float` | Horizontal position in pixels from upper-left. |
| `Y` | `float` | Vertical position in pixels from upper-left. |
| `PostUpdate()` | `void` | Called after `Update()`. Use for bounds checking, collision, rendering. |

**Properties:**

| Property | Type | Default | Description |
|---|---|---|---|
| `Speed` | `float` (virtual) | `0` | Bullet speed in pixels/frame. |
| `Direction` | `float` (virtual) | `0` | Direction in radians. Automatically wrapped via `MathHelper.WrapAngle`. |
| `Acceleration` | `Vector2` | `Zero` | Acceleration vector in pixels/frame^2. Set by `<accel>` nodes. |
| `TimeSpeed` | `float` | `1.0` | Time multiplier. Values < 1 slow down, > 1 speed up. |
| `Scale` | `float` | `1.0` | Pattern size multiplier. |
| `InitialVelocity` | `Vector2` | `Zero` | Velocity inherited from the firing entity. Applied during `InitNode`. |
| `Tasks` | `List<BulletMLTask>` | `[]` | Runtime tasks defining this bullet's behavior. |
| `MyNode` | `BulletMLNode` | `null` | The node tree describing this bullet. |
| `MyBulletManager` | `IBulletManager` | -- | The manager that owns this bullet. |
| `Label` | `string` | -- | Convenience property returning `MyNode.Label`. |

**Methods:**

| Method | Returns | Description |
|---|---|---|
| `InitTopNode(BulletMLNode rootNode)` | `void` | Initialize from a root node. Finds nodes labeled `"top"` or `"top1"` through `"top9"`. |
| `InitNode(BulletMLNode subNode)` | `void` | Initialize from a specific sub-node. Clears existing tasks and builds new task tree. |
| `Update()` | `void` (virtual) | Execute one frame: runs all tasks, then updates position from velocity and acceleration. |
| `UpdateAsync()` | `Task` | Runs `Update()` on a background thread. |
| `GetAimDir()` | `float` (virtual) | Returns the angle (radians) from this bullet to the player position. |
| `TasksFinished()` | `bool` | Returns `true` when all tasks have completed execution. |
| `FindTaskByLabel(string label)` | `BulletMLTask` | Recursively searches tasks for a matching label. |
| `FindTaskByLabelAndName(string label, NodeName name)` | `BulletMLTask` | Searches tasks matching both label and node name. |

**Position Update Formula:**

Each frame, the bullet's position is updated as:

```
velocity = (Acceleration + Direction.ToVector2() * Speed * TimeSpeed) * Scale
X += velocity.X
Y += velocity.Y
```

---

### BulletPattern

```csharp
public class BulletPattern
```

Container for a parsed BulletML document. Parse once, use many times to initialize bullets.

**Constructor:**

```csharp
public BulletPattern(IBulletManager manager)
```

**Properties:**

| Property | Type | Description |
|---|---|---|
| `RootNode` | `BulletMLNode` | Root of the parsed node tree. Pass to `Bullet.InitTopNode()`. |
| `Filename` | `string` | Path of the loaded XML file. Set by `ParseXML()`. |
| `Orientation` | `PatternType` | Pattern type read from the `<bulletml type="...">` attribute. |

**Methods:**

| Method | Description |
|---|---|
| `ParseXML(string xmlFileName, ContentManager content = null)` | Parse a BulletML XML file. If `content` is provided, loads via MonoGame Content Pipeline (path should be relative, no extension). Otherwise loads from file system. |

---

### BulletMLNode

```csharp
public class BulletMLNode
```

Represents a single element in the parsed BulletML document tree. Nodes are shared across bullets; tasks are created per-bullet at runtime.

**Properties:**

| Property | Type | Description |
|---|---|---|
| `Name` | `NodeName` | The type of this node (bullet, action, fire, etc.). |
| `NodeType` | `NodeType` | Modifier type (none, aim, absolute, relative, sequence). |
| `Label` | `string` | The label attribute from the XML element. |
| `ChildNodes` | `List<BulletMLNode>` | Child nodes in the tree. |

**Methods:**

| Method | Description |
|---|---|
| `FindLabelNode(string label, NodeName name)` | Find a descendant node with matching label and name. |
| `FindParentNode(NodeName name)` | Walk up the tree to find an ancestor of the given type. |
| `GetChild(NodeName name)` | Find the first direct child with the given name. |

---

### BulletMLTask

```csharp
public class BulletMLTask
```

Runtime execution unit created from a `BulletMLNode`. Each bullet gets its own task tree.

**Properties:**

| Property | Type | Description |
|---|---|---|
| `Node` | `BulletMLNode` | The node this task was created from. |
| `Owner` | `BulletMLTask` | Parent task in the execution tree. |
| `ChildTasks` | `List<BulletMLTask>` | Child tasks to execute. |
| `TaskFinished` | `bool` | Whether this task (and all children) have completed. |
| `ParamList` | `List<float>` | Parameter values for referenced nodes (accessible as `$1`, `$2`, etc.). |

---

### FireData

```csharp
public class FireData
```

Template data for creating new bullets during pattern execution.

**Fields:**

| Field | Type | Description |
|---|---|---|
| `srcSpeed` | `float` | Initial speed for fired bullets. |
| `srcDir` | `float` | Initial direction for fired bullets. |
| `speedInit` | `bool` | Whether speed has been explicitly set. |

---

## Interfaces

### IBullet

```csharp
public interface IBullet
```

Interface for bullet-like objects. Implement this if you want to wrap a `Bullet` in another object.

| Member | Type | Description |
|---|---|---|
| `X` | `float` | Horizontal position (pixels from upper-left). |
| `Y` | `float` | Vertical position (pixels from upper-left). |
| `Speed` | `float` | Speed in pixels/frame. |
| `Direction` | `float` | Direction in radians. |
| `Tasks` | `List<BulletMLTask>` | Runtime task list. |
| `InitTopNode(BulletMLNode)` | `void` | Initialize from a root node. |
| `InitNode(BulletMLNode)` | `void` | Initialize from a sub-node. |

---

### IBulletManager

```csharp
public interface IBulletManager
```

Interface your game implements to manage bullet lifecycle and provide game state to the library.

| Member | Type | Description |
|---|---|---|
| `Rand` | `Random` | Random number generator for `$rand` in expressions. |
| `GameDifficulty` | `FunctionDelegate` | Returns difficulty value (typically 0.0-1.0) for `$rank` in expressions. |
| `CallbackFunctions` | `Dictionary<string, FunctionDelegate>` | Custom functions accessible as `$name` in BulletML expressions. |
| `PlayerPosition(IBullet)` | `Vector2` | Returns the target position for `aim`-type directions. |
| `CreateBullet()` | `IBullet` | Factory for regular (visible) bullets. Called by `<fire>` nodes. |
| `CreateTopBullet()` | `IBullet` | Factory for top-level controller bullets (usually invisible). |
| `RemoveBullet(IBullet)` | `void` | Dispose/deactivate a bullet. Called by `<vanish>` nodes. |

---

## Enums

### NodeName

Identifies the type of a BulletML XML element.

| Value | XML Element | Description |
|---|---|---|
| `bulletml` | `<bulletml>` | Root document element. |
| `bullet` | `<bullet>` | Bullet definition with direction, speed, and actions. |
| `action` | `<action>` | Sequence of commands defining bullet behavior. |
| `fire` | `<fire>` | Fires a new bullet with optional direction/speed. |
| `direction` | `<direction>` | Direction value (with type modifier). |
| `speed` | `<speed>` | Speed value (with type modifier). |
| `changeDirection` | `<changeDirection>` | Gradually change direction over a duration. |
| `changeSpeed` | `<changeSpeed>` | Gradually change speed over a duration. |
| `accel` | `<accel>` | Apply acceleration (horizontal/vertical components). |
| `wait` | `<wait>` | Pause execution for N frames. |
| `repeat` | `<repeat>` | Repeat child actions N times. |
| `vanish` | `<vanish>` | Remove the bullet. |
| `horizontal` | `<horizontal>` | Horizontal acceleration component (inside `<accel>`). |
| `vertical` | `<vertical>` | Vertical acceleration component (inside `<accel>`). |
| `term` | `<term>` | Duration for changeDirection/changeSpeed/accel. |
| `times` | `<times>` | Repeat count. |
| `param` | `<param>` | Parameter value for referenced nodes. |
| `bulletRef` | `<bulletRef>` | Reference to a labeled `<bullet>`. |
| `actionRef` | `<actionRef>` | Reference to a labeled `<action>`. |
| `fireRef` | `<fireRef>` | Reference to a labeled `<fire>`. |

### NodeType

Modifier for direction and speed values.

| Value | Description |
|---|---|
| `none` | No modifier (default). |
| `aim` | Direction aimed at the player position. |
| `absolute` | Absolute value (0 = up for vertical patterns, right for horizontal). |
| `relative` | Relative to the bullet's current direction or speed. |
| `sequence` | Added to the previous value each time the node fires. |

### PatternType

Orientation of the bullet pattern, set on the `<bulletml>` root element.

| Value | Description |
|---|---|
| `vertical` | Bullets travel top-to-bottom. 0 degrees = down. |
| `horizontal` | Bullets travel left-to-right. 0 degrees = right. |
| `none` | No specific orientation. |

### RunStatus

Internal enum for task execution state.

| Value | Description |
|---|---|
| `Continue` | Task is still running. |
| `End` | Task has finished. |
| `Stop` | Task has been stopped. |

---

## Delegates

### PositionDelegate

```csharp
public delegate Vector2 PositionDelegate();
```

Callback that returns a `Vector2` position.

### FloatDelegate

```csharp
public delegate float FloatDelegate();
```

Callback that returns a `float` value.

### FunctionDelegate

```csharp
// From Equationator library
public delegate float FunctionDelegate();
```

Used for `GameDifficulty` and entries in `CallbackFunctions`. Returns a `float` value that can be referenced in BulletML expressions.
