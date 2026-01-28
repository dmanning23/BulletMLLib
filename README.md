# BulletMLLib

A C# library for parsing and executing [BulletML](http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/index_e.html) XML files. Define complex bullet patterns declaratively in XML and run them in your shmup (shoot-em-up) game.

Based on the implementation by Keiichi Kashihara of [Bandle Games](https://sites.google.com/site/bandlegames/bulletml-c).

## Features

- Parse BulletML XML documents into executable pattern trees
- All BulletML elements supported: fire, direction, speed, acceleration, repeat, vanish, change direction/speed, and more
- Parameterized references for reusable bullet definitions
- Expression evaluation in node values (`$rank`, `$rand`, custom callbacks)
- Configurable time speed (slow-motion) and scale (resize patterns)
- Async update support

## Installation

```
dotnet add package BulletMLLib
```

**Requirements:** .NET 8.0+, [MonoGame.Framework.DesktopGL](https://www.monogame.net/) 3.8+

## Quick Start

**1. Implement `IBulletManager`** to handle bullet lifecycle and provide game state:

```csharp
public class MyBulletManager : IBulletManager
{
    public Random Rand { get; } = new Random();
    public Dictionary<string, FunctionDelegate> CallbackFunctions { get; } = new();
    public FunctionDelegate GameDifficulty { get; set; } = () => 0.5f;

    public Vector2 PlayerPosition(IBullet bullet) => _playerPos;
    public IBullet CreateBullet() => new MyBullet(this);
    public IBullet CreateTopBullet() => new MyBullet(this);
    public void RemoveBullet(IBullet bullet) { /* mark inactive */ }
}
```

**2. Inherit from `Bullet`** to provide position storage and bounds checking:

```csharp
public class MyBullet : Bullet
{
    private Vector2 _position;
    public override float X { get => _position.X; set => _position.X = value; }
    public override float Y { get => _position.Y; set => _position.Y = value; }

    public MyBullet(IBulletManager manager) : base(manager) { }
    public override void PostUpdate() { /* check bounds, collisions, etc. */ }
}
```

**3. Load a pattern and fire it:**

```csharp
var manager = new MyBulletManager();
var pattern = new BulletPattern(manager);
pattern.ParseXML("patterns/spiral.xml");

var topBullet = manager.CreateTopBullet();
topBullet.X = 400;
topBullet.Y = 100;
topBullet.InitTopNode(pattern.RootNode);

// Each frame:
topBullet.Update();
```

For a complete working example, see the [QuickStart project](https://github.com/dmanning23/BulletMLLibQuickStart).

## Example BulletML Pattern

A spiral pattern that continuously fires rotating bullets:

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

## Documentation

Full documentation is available in the [docs](docs/) folder:

- [Getting Started](docs/getting-started.md) -- step-by-step integration guide
- [API Reference](docs/api-reference.md) -- classes, interfaces, enums, and properties
- [BulletML Pattern Guide](docs/bulletml-guide.md) -- writing BulletML XML scripts
- [Architecture](docs/architecture.md) -- library internals and design

The [wiki](https://github.com/dmanning23/BulletMLLib/wiki) is also a useful reference for writing BulletML scripts.

## Resources

- [BulletML Specification](http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/index_e.html) -- original language spec by ABA Games
- [BulletML Examples](https://github.com/dmanning23/BulletMLExamples) -- collection of open-source BulletML patterns
- [QuickStart Project](https://github.com/dmanning23/BulletMLLibQuickStart) -- minimal working example

## License

MIT
