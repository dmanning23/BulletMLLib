# BulletMLLib

A C# library for parsing and executing [BulletML](http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/index_e.html) XML files. Define complex bullet patterns declaratively in XML and execute them at runtime in your shmup (shoot-em-up) game.

## Features

- Parse BulletML XML documents into an executable node tree
- Execute bullet patterns with per-frame updates
- Support for all BulletML elements: fire, direction, speed, acceleration, repeat, vanish, and more
- Parameterized pattern references for reusable bullet definitions
- Mathematical expression evaluation in node values (including `$rank`, `$rand`, and custom callbacks)
- Configurable time speed (slowdown/speedup) and scale (resize patterns)
- Async update support
- Built on MonoGame/XNA types

## Requirements

- .NET 8.0 or later
- [MonoGame.Framework.DesktopGL](https://www.monogame.net/) 3.8+
- [Equationator](https://github.com/dmanning23/Equationator) 5.x (mathematical expression parser)
- [Vector2Extensions](https://github.com/dmanning23/Vector2Extensions) 5.x

## Installation

Install via NuGet:

```
dotnet add package BulletMLLib
```

Or add to your `.csproj`:

```xml
<PackageReference Include="BulletMLLib" Version="5.*" />
```

## Quick Example

```csharp
// 1. Implement IBulletManager
public class MyBulletManager : IBulletManager
{
    public Random Rand { get; } = new Random();
    public Dictionary<string, FunctionDelegate> CallbackFunctions { get; } = new();
    public FunctionDelegate GameDifficulty { get; set; }

    public Vector2 PlayerPosition(IBullet bullet) => _playerPos;
    public IBullet CreateBullet() => new MyBullet(this);
    public IBullet CreateTopBullet() => new MyBullet(this);
    public void RemoveBullet(IBullet bullet) { /* mark inactive */ }
}

// 2. Inherit from Bullet
public class MyBullet : Bullet
{
    private Vector2 _position;
    public override float X { get => _position.X; set => _position.X = value; }
    public override float Y { get => _position.Y; set => _position.Y = value; }

    public MyBullet(IBulletManager manager) : base(manager) { }
    public override void PostUpdate() { /* check bounds, draw, etc. */ }
}

// 3. Load and run a pattern
var manager = new MyBulletManager();
var pattern = new BulletPattern(manager);
pattern.ParseXML("patterns/spiral.xml");

var topBullet = manager.CreateTopBullet();
topBullet.X = 200;
topBullet.Y = 50;
topBullet.InitTopNode(pattern.RootNode);

// 4. Each frame:
topBullet.Update();
```

## Documentation

- [Getting Started](getting-started.md) -- step-by-step integration guide
- [API Reference](api-reference.md) -- classes, interfaces, enums, and properties
- [BulletML Pattern Guide](bulletml-guide.md) -- writing BulletML XML scripts
- [Architecture](architecture.md) -- library internals and design

## Links

- [GitHub Repository](https://github.com/dmanning23/BulletMLLib)
- [BulletML Specification](http://www.asahi-net.or.jp/~cs8k-cyu/bulletml/index_e.html)
- [BulletML Examples](https://github.com/dmanning23/BulletMLExamples)
- [QuickStart Project](https://github.com/dmanning23/BulletMLLibQuickStart)

## License

MIT
