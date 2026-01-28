using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using System;
using BulletMLLib;

namespace BulletMLTests
{
    [TestFixture()]
    public class FireRefTest
    {
        MoverManager manager;
        Myship dude;
        BulletPattern pattern;

        [SetUp()]
        public void setupHarness()
        {
            dude = new Myship();
            manager = new MoverManager(dude.Position);
            pattern = new BulletPattern(manager);
        }

        [Test()]
        public void CorrectBullets()
        {
            var filename = new Filename(@"FireRef.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            manager.movers.Count.ShouldBe(2);

            mover = manager.movers[1];
            mover.Label.ShouldBe("testBullet");
        }

        [Test()]
        public void CorrectSpeedFromParam()
        {
            var filename = new Filename(@"FireRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            manager.movers.Count.ShouldBe(2);

            mover = manager.movers[1];
            mover.Label.ShouldBe("testBullet");
            mover.Speed.ShouldBe(15.0f);
        }
    }
}

