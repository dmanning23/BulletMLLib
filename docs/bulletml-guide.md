# BulletML Pattern Guide

A guide to writing BulletML XML files for use with BulletMLLib. BulletML is a markup language for describing bullet patterns (barrage) in shmup games, originally created by Kenta Cho (ABA Games).

## Document Structure

Every BulletML file starts with the `<bulletml>` root element:

```xml
<?xml version="1.0"?>
<!DOCTYPE bulletml SYSTEM "bulletml.dtd">
<bulletml type="vertical" xmlns="http://www.asahi-net.or.jp/~cs8k-cyu/bulletml">
  <!-- pattern definitions go here -->
</bulletml>
```

The `type` attribute sets the coordinate orientation:
- `vertical` -- bullets travel top-to-bottom (most common). 0 degrees = down.
- `horizontal` -- bullets travel left-to-right. 0 degrees = right.

## Entry Points

The library looks for action nodes labeled `"top"` as the entry point. A pattern must have at least one:

```xml
<action label="top">
  <!-- this action runs when the pattern starts -->
</action>
```

For patterns with multiple simultaneous actions, use `"top1"` through `"top9"`:

```xml
<action label="top1">
  <!-- first concurrent action -->
</action>
<action label="top2">
  <!-- second concurrent action -->
</action>
```

Each top-level action runs on its own bullet.

## Core Elements

### `<fire>` -- Create a Bullet

Fires a new bullet. Specify direction, speed, and the bullet definition:

```xml
<fire>
  <direction type="aim">0</direction>
  <speed>3</speed>
  <bullet />
</fire>
```

The `<bullet />` can be inline (empty or with child actions) or a reference:

```xml
<fire>
  <bulletRef label="bigBullet" />
</fire>
```

### `<bullet>` -- Define a Bullet

Defines a bullet's properties and behavior:

```xml
<bullet label="bigBullet">
  <direction type="absolute">180</direction>
  <speed>2</speed>
  <action>
    <wait>60</wait>
    <vanish />
  </action>
</bullet>
```

A bullet can have:
- `<direction>` -- initial direction
- `<speed>` -- initial speed
- One or more `<action>` blocks -- behavior after being fired

### `<action>` -- Behavior Sequence

A sequence of commands executed in order:

```xml
<action label="top">
  <fire>
    <bullet />
  </fire>
  <wait>10</wait>
  <fire>
    <direction type="aim">0</direction>
    <bullet />
  </fire>
</action>
```

Actions can contain: `<fire>`, `<wait>`, `<repeat>`, `<vanish>`, `<changeDirection>`, `<changeSpeed>`, `<accel>`, nested `<action>`, and references (`<fireRef>`, `<actionRef>`).

### `<wait>` -- Pause Execution

Pauses the current action for a number of frames:

```xml
<wait>30</wait>  <!-- wait half a second at 60fps -->
```

### `<vanish>` -- Remove the Bullet

Removes the current bullet from the game:

```xml
<vanish />
```

### `<repeat>` -- Loop

Repeats a child action N times:

```xml
<repeat>
  <times>10</times>
  <action>
    <fire><bullet /></fire>
    <wait>5</wait>
  </action>
</repeat>
```

## Direction and Speed Types

Both `<direction>` and `<speed>` accept a `type` attribute that changes how the value is interpreted.

### Direction Types

| Type | Description | Example |
|---|---|---|
| `aim` | Angle relative to the player. 0 = directly at player. | `<direction type="aim">15</direction>` fires 15 degrees off from the player. |
| `absolute` | Fixed angle. In vertical mode: 0 = up, 90 = right, 180 = down. | `<direction type="absolute">180</direction>` fires straight down. |
| `relative` | Added to the current bullet's direction. | `<direction type="relative">45</direction>` turns 45 degrees clockwise from current heading. |
| `sequence` | Added to the previous fire's direction each time. | `<direction type="sequence">10</direction>` each subsequent fire rotates 10 degrees. |

If no type is specified, `aim` is the default for `<direction>`.

### Speed Types

| Type | Description |
|---|---|
| `absolute` | Fixed speed in pixels/frame. |
| `relative` | Added to the current bullet's speed. |
| `sequence` | Added to the previous fire's speed each time. |

If no type is specified, `absolute` is the default for `<speed>`.

## Motion Modification

### `<changeDirection>` -- Gradual Turn

Smoothly changes direction over a duration:

```xml
<changeDirection>
  <direction type="aim">0</direction>
  <term>60</term>
</changeDirection>
```

This turns toward the player over 60 frames. The direction type works the same as in `<fire>`.

### `<changeSpeed>` -- Gradual Acceleration

Smoothly changes speed over a duration:

```xml
<changeSpeed>
  <speed>5</speed>
  <term>30</term>
</changeSpeed>
```

Changes to speed 5 over 30 frames.

### `<accel>` -- Direct Acceleration

Applies horizontal and vertical acceleration:

```xml
<accel>
  <horizontal type="absolute">1</horizontal>
  <vertical type="absolute">2</vertical>
  <term>30</term>
</accel>
```

Accelerates for 30 frames. The `<horizontal>` and `<vertical>` elements support `absolute`, `relative`, and `sequence` types.

## References and Parameters

### Action References

Define an action once, reuse it multiple times:

```xml
<action label="circle">
  <repeat>
    <times>36</times>
    <action>
      <fire>
        <direction type="sequence">10</direction>
        <bullet />
      </fire>
    </action>
  </repeat>
</action>

<action label="top">
  <actionRef label="circle" />
  <wait>30</wait>
  <actionRef label="circle" />
</action>
```

### Parameterized References

Pass values to referenced actions using `<param>`:

```xml
<action label="nWay">
  <!-- $1 = number of bullets, $2 = spread angle -->
  <repeat>
    <times>$1</times>
    <action>
      <fire>
        <direction type="sequence">$2</direction>
        <bullet />
      </fire>
    </action>
  </repeat>
</action>

<action label="top">
  <actionRef label="nWay">
    <param>5</param>    <!-- $1 = 5 bullets -->
    <param>15</param>   <!-- $2 = 15 degree spread -->
  </actionRef>
</action>
```

Parameters are numbered `$1`, `$2`, etc. in order.

### Bullet References

Reference pre-defined bullets:

```xml
<bullet label="tracker">
  <speed>2</speed>
  <action>
    <changeDirection>
      <direction type="aim">0</direction>
      <term>30</term>
    </changeDirection>
  </action>
</bullet>

<action label="top">
  <fire>
    <direction type="aim">0</direction>
    <bulletRef label="tracker" />
  </fire>
</action>
```

### Fire References

Reference pre-defined fire patterns:

```xml
<fire label="aimShot">
  <direction type="aim">0</direction>
  <speed>3</speed>
  <bullet />
</fire>

<action label="top">
  <repeat>
    <times>5</times>
    <action>
      <fireRef label="aimShot" />
      <wait>10</wait>
    </action>
  </repeat>
</action>
```

## Expressions

Node values support mathematical expressions, not just constants.

### Built-in Variables

| Variable | Description |
|---|---|
| `$rank` | Game difficulty (set via `IBulletManager.GameDifficulty`). Typically 0.0 - 1.0. |
| `$rand` | Random value between 0.0 and 1.0 (uses `IBulletManager.Rand`). |
| `$1`, `$2`, ... | Parameter values passed via `<param>` in references. |

### Custom Variables

Any function added to `IBulletManager.CallbackFunctions` is accessible as `$name`:

```csharp
manager.CallbackFunctions["tier"] = () => playerTier;
```

```xml
<speed>1 + $tier</speed>
```

### Expression Examples

```xml
<!-- Speed scales with difficulty -->
<speed>1 + 3 * $rank</speed>

<!-- Random spread -->
<direction type="aim">-30 + 60 * $rand</direction>

<!-- Wait time decreases with difficulty -->
<wait>30 - 20 * $rank</wait>

<!-- Use a parameter -->
<times>$1</times>
```

## Complete Examples

### Aimed Single Shot

Fires a single bullet aimed at the player every 30 frames:

```xml
<?xml version="1.0"?>
<bulletml type="vertical">
  <action label="top">
    <repeat>
      <times>999</times>
      <action>
        <fire>
          <direction type="aim">0</direction>
          <speed>3</speed>
          <bullet />
        </fire>
        <wait>30</wait>
      </action>
    </repeat>
  </action>
</bulletml>
```

### 360-Degree Circle

Fires a ring of 36 bullets:

```xml
<?xml version="1.0"?>
<bulletml type="vertical">
  <action label="top">
    <repeat>
      <times>999</times>
      <action>
        <fire>
          <direction type="absolute">0</direction>
          <speed>2</speed>
          <bullet />
        </fire>
        <repeat>
          <times>35</times>
          <action>
            <fire>
              <direction type="sequence">10</direction>
              <speed type="sequence">0</speed>
              <bullet />
            </fire>
          </action>
        </repeat>
        <wait>60</wait>
      </action>
    </repeat>
  </action>
</bulletml>
```

### Spiral Pattern

Fires a continuous rotating stream:

```xml
<?xml version="1.0"?>
<bulletml type="vertical">
  <action label="top">
    <repeat>
      <times>999</times>
      <action>
        <fire>
          <direction type="sequence">13</direction>
          <speed>2</speed>
          <bullet />
        </fire>
        <wait>2</wait>
      </action>
    </repeat>
  </action>
</bulletml>
```

The `sequence` direction adds 13 degrees per shot, creating a spiral.

### Aimed N-Way with Difficulty Scaling

Fires a spread of bullets aimed at the player, count scales with rank:

```xml
<?xml version="1.0"?>
<bulletml type="vertical">
  <action label="nWay">
    <fire>
      <direction type="aim">-10 * $1</direction>
      <speed>3</speed>
      <bullet />
    </fire>
    <repeat>
      <times>$1 * 2</times>
      <action>
        <fire>
          <direction type="sequence">10</direction>
          <speed type="sequence">0</speed>
          <bullet />
        </fire>
      </action>
    </repeat>
  </action>

  <action label="top">
    <repeat>
      <times>999</times>
      <action>
        <actionRef label="nWay">
          <param>1 + 3 * $rank</param>
        </actionRef>
        <wait>30 - 15 * $rank</wait>
      </action>
    </repeat>
  </action>
</bulletml>
```

### Homing Bullet

A bullet that tracks the player for a duration, then flies straight:

```xml
<?xml version="1.0"?>
<bulletml type="vertical">
  <bullet label="homing">
    <speed>2</speed>
    <action>
      <changeDirection>
        <direction type="aim">0</direction>
        <term>90</term>
      </changeDirection>
      <wait>90</wait>
      <!-- after 90 frames, stop homing and fly straight -->
    </action>
  </bullet>

  <action label="top">
    <repeat>
      <times>999</times>
      <action>
        <fire>
          <direction type="aim">0</direction>
          <bulletRef label="homing" />
        </fire>
        <wait>60</wait>
      </action>
    </repeat>
  </action>
</bulletml>
```

## Tips

- Use `<repeat><times>999</times>...</repeat>` for patterns that should run indefinitely.
- The `sequence` type is powerful for creating fans and spirals -- each `<fire>` in a loop adds the sequence value to the previous direction/speed.
- Use `$rank` to scale difficulty. Patterns that use `$rank` can work across all difficulty levels from a single XML file.
- Keep patterns modular with `<actionRef>` and `<bulletRef>`. Define base behaviors once and compose them.
- Test patterns with different `$rank` values (0.0, 0.5, 1.0) to verify difficulty scaling.

## Further Reading

- [BulletML Specification](http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/index_e.html) -- original language spec by ABA Games
- [BulletML Examples](https://github.com/dmanning23/BulletMLExamples) -- collection of open-source BulletML patterns
