# Getting Started

This guide walks through integrating BulletMLLib into a MonoGame/XNA project.

## Overview

BulletMLLib requires two things from your game:

1. **A bullet class** that inherits from `Bullet` (provides position, rendering, and lifecycle)
2. **A bullet manager** that implements `IBulletManager` (creates/destroys bullets, provides player position)

Once those are in place, you load BulletML XML files into `BulletPattern` objects and initialize bullets from them.

## Step 1: Create Your Bullet Class

Inherit from `Bullet` and implement the abstract members. The library handles direction, speed, acceleration, and task execution. You provide position storage, rendering, and bounds checking.

```csharp
using BulletMLLib;
using Microsoft.Xna.Framework;

public class Mover : Bullet
{
    private Vector2 _position;

    /// <summary>
    /// Whether this bullet is currently active in the game.
    /// </summary>
    public bool Used { get; set; }

    public override float X
    {
        get => _position.X;
        set => _position.X = value;
    }

    public override float Y
    {
        get => _position.Y;
        set => _position.Y = value;
    }

    public Mover(IBulletManager manager) : base(manager)
    {
        Used = true;
    }

    /// <summary>
    /// Called after Update(). Use for bounds checking, collision, etc.
    /// </summary>
    public override void PostUpdate()
    {
        // Remove bullets that leave the screen
        if (X < 0 || X > 800 || Y < 0 || Y > 600)
        {
            Used = false;
        }
    }
}
```

### Key Points

- `X` and `Y` are measured in pixels from the upper-left corner.
- `Speed` is in pixels per frame.
- `Direction` is in radians.
- `PostUpdate()` is called after the library updates position. Use it for bounds checking or collision detection.
- The base `Bullet` constructor requires an `IBulletManager` reference.

### Optional Properties

| Property | Type | Default | Description |
|---|---|---|---|
| `TimeSpeed` | `float` | `1.0` | Multiplier for bullet speed. Use for slow-motion effects. |
| `Scale` | `float` | `1.0` | Multiplier for pattern size. Resize patterns without editing XML. |
| `InitialVelocity` | `Vector2` | `Zero` | Inherit velocity from the firing entity (e.g., a moving enemy). |
| `Acceleration` | `Vector2` | `Zero` | Current acceleration vector (set by `<accel>` nodes). |

## Step 2: Implement IBulletManager

The bullet manager is how the library communicates with your game. It handles bullet lifecycle and provides game state.

```csharp
using BulletMLLib;
using Equationator;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

public class MoverManager : IBulletManager
{
    /// <summary>Active bullets in the game.</summary>
    public List<Mover> Movers { get; } = new();

    /// <summary>Top-level bullets (pattern controllers, usually invisible).</summary>
    public List<Mover> TopLevelMovers { get; } = new();

    public Random Rand { get; } = new Random();

    public FunctionDelegate GameDifficulty { get; set; }

    public Dictionary<string, FunctionDelegate> CallbackFunctions { get; } = new();

    /// <summary>Time speed applied to newly created bullets.</summary>
    public float TimeSpeed { get; set; } = 1.0f;

    /// <summary>Scale applied to newly created bullets.</summary>
    public float Scale { get; set; } = 1.0f;

    private Vector2 _playerPosition;

    public MoverManager(Vector2 playerPos)
    {
        _playerPosition = playerPos;

        // GameDifficulty returns a value 0.0-1.0 used by $rank in BulletML
        GameDifficulty = () => 0.5f;
    }

    public Vector2 PlayerPosition(IBullet bullet)
    {
        return _playerPosition;
    }

    public IBullet CreateBullet()
    {
        var mover = new Mover(this)
        {
            TimeSpeed = TimeSpeed,
            Scale = Scale
        };
        Movers.Add(mover);
        return mover;
    }

    public IBullet CreateTopBullet()
    {
        var mover = new Mover(this)
        {
            TimeSpeed = TimeSpeed,
            Scale = Scale
        };
        TopLevelMovers.Add(mover);
        return mover;
    }

    public void RemoveBullet(IBullet bullet)
    {
        var mover = bullet as Mover;
        if (mover != null)
        {
            mover.Used = false;
        }
    }

    public void Update()
    {
        // Update all active bullets
        for (int i = 0; i < Movers.Count; i++)
        {
            Movers[i].Update();
        }
        for (int i = 0; i < TopLevelMovers.Count; i++)
        {
            TopLevelMovers[i].Update();
        }

        // Remove inactive bullets
        Movers.RemoveAll(m => !m.Used);
        TopLevelMovers.RemoveAll(m => !m.Used);
    }
}
```

### IBulletManager Members

| Member | Purpose |
|---|---|
| `Rand` | Random number generator used by `$rand` in BulletML expressions. |
| `GameDifficulty` | Returns a `float` (typically 0.0-1.0) used by `$rank` in BulletML expressions. |
| `CallbackFunctions` | Dictionary of custom functions accessible in BulletML expressions (e.g., `$tier`). |
| `PlayerPosition(IBullet)` | Returns the position that `aim`-type directions target. |
| `CreateBullet()` | Factory method called when a `<fire>` node creates a new bullet. |
| `CreateTopBullet()` | Factory for top-level pattern controller bullets (typically invisible). |
| `RemoveBullet(IBullet)` | Called when a `<vanish>` node removes a bullet or a pattern finishes. |

### Top-Level vs Regular Bullets

- **Top-level bullets** (`CreateTopBullet`) are pattern controllers. They execute the BulletML action tree but usually aren't rendered. They act as the "gun" that fires visible bullets.
- **Regular bullets** (`CreateBullet`) are the projectiles that players see and interact with. They are created by `<fire>` elements during pattern execution.

## Step 3: Load and Run Patterns

```csharp
// Create the manager
var manager = new MoverManager(playerShip.Position);

// Load a pattern from XML
var pattern = new BulletPattern(manager);
pattern.ParseXML("Content/Samples/spiral.xml");

// Create a top-level bullet to run the pattern
var topBullet = (Mover)manager.CreateTopBullet();
topBullet.X = 400;  // Pattern origin X
topBullet.Y = 100;  // Pattern origin Y
topBullet.InitTopNode(pattern.RootNode);

// In your game loop (called each frame):
manager.Update();
```

### Loading from MonoGame Content Pipeline

If you're using the MonoGame Content Pipeline, pass the `ContentManager`:

```csharp
pattern.ParseXML("Samples/spiral", Content);
```

When using the content pipeline, provide the relative path without the file extension.

### Running Multiple Patterns

You can load multiple patterns and assign them to different enemies:

```csharp
var patterns = new List<BulletPattern>();
foreach (var file in Directory.GetFiles("Content/Patterns", "*.xml"))
{
    var p = new BulletPattern(manager);
    p.ParseXML(file);
    patterns.Add(p);
}

// Each enemy can fire a different pattern
void SpawnEnemy(Vector2 position, BulletPattern pattern)
{
    var bullet = (Mover)manager.CreateTopBullet();
    bullet.X = position.X;
    bullet.Y = position.Y;
    bullet.InitTopNode(pattern.RootNode);
}
```

### Pattern Lifecycle

1. `BulletPattern.ParseXML()` parses the XML once into a node tree (reusable).
2. `bullet.InitTopNode()` creates a runtime task tree from the node tree.
3. `bullet.Update()` executes one frame of the task tree.
4. When all tasks finish, `bullet.TasksFinished()` returns `true`.

## Step 4: Adjust Difficulty and Scale

### Difficulty (Rank)

BulletML scripts can use `$rank` in expressions to scale difficulty. Set it via `GameDifficulty`:

```csharp
// 0.0 = easiest, 1.0 = hardest
manager.GameDifficulty = () => currentDifficulty;
```

For example, a BulletML script might use: `<speed>1 + 3 * $rank</speed>` to fire bullets at speed 1 on easy and speed 4 on hard.

### Time Speed

Slow down or speed up pattern execution:

```csharp
manager.TimeSpeed = 0.5f;  // Half speed (slow motion)
manager.TimeSpeed = 2.0f;  // Double speed
```

### Scale

Resize a pattern without editing the XML:

```csharp
manager.Scale = 0.5f;  // Half size
manager.Scale = 2.0f;  // Double size
```

### Custom Callback Functions

Add game-specific values accessible in BulletML expressions:

```csharp
manager.CallbackFunctions["tier"] = () => playerTier;
manager.CallbackFunctions["level"] = () => currentLevel;
```

These can then be used in BulletML as `$tier` and `$level`.

## Step 5: Async Updates

For games with many bullets, you can update asynchronously:

```csharp
await bullet.UpdateAsync();
```

This runs `Update()` on a background thread via `Task.Factory.StartNew`.

## Next Steps

- [BulletML Pattern Guide](bulletml-guide.md) -- learn to write bullet patterns
- [API Reference](api-reference.md) -- full class and interface documentation
- [Architecture](architecture.md) -- understand how the library works internally
