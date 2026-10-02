using BulletMLLib;
using Equationator;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace BulletMLTests
{
    [TestFixture()]
    public class NullManagerTest
    {
        /// <summary>
        /// A bullet manager where any of the members can be left null
        /// </summary>
        class StubManager : IBulletManager
        {
            public Random Rand { get; set; } = new Random();

            public Dictionary<string, FunctionDelegate> CallbackFunctions { get; set; } = new Dictionary<string, FunctionDelegate>();

            public FunctionDelegate GameDifficulty { get; set; } = () => 0.0;

            public Vector2 PlayerPosition(IBullet targettedBullet)
            {
                return Vector2.Zero;
            }

            public void RemoveBullet(IBullet deadBullet)
            {
            }

            public IBullet CreateBullet()
            {
                return null;
            }

            public IBullet CreateTopBullet()
            {
                return null;
            }
        }

        [Test()]
        public void ValidManagerDoesNotThrow()
        {
            Should.NotThrow(() => new BulletMLEquation(new StubManager()));
        }

        [Test()]
        public void PatternWithNullManagerThrows()
        {
            var ex = Should.Throw<ArgumentNullException>(() => new BulletPattern(null));
            ex.ParamName.ShouldBe("manager");
        }

        [Test()]
        public void EquationWithNullManagerThrows()
        {
            var ex = Should.Throw<ArgumentNullException>(() => new BulletMLEquation(null));
            ex.ParamName.ShouldBe("manager");
        }

        [Test()]
        public void NullRandThrows()
        {
            var manager = new StubManager() { Rand = null };
            var ex = Should.Throw<ArgumentException>(() => new BulletMLEquation(manager));
            ex.Message.ShouldContain("IBulletManager.Rand");
        }

        [Test()]
        public void NullCallbackFunctionsThrows()
        {
            var manager = new StubManager() { CallbackFunctions = null };
            var ex = Should.Throw<ArgumentException>(() => new BulletMLEquation(manager));
            ex.Message.ShouldContain("IBulletManager.CallbackFunctions");
        }

        [Test()]
        public void NullGameDifficultyThrows()
        {
            var manager = new StubManager() { GameDifficulty = null };
            var ex = Should.Throw<ArgumentException>(() => new BulletMLEquation(manager));
            ex.Message.ShouldContain("IBulletManager.GameDifficulty");
        }
    }
}
