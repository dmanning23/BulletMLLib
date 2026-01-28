using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class ActionRefTest
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
            var filename = new Filename(@"ActionRefParamChangeSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            manager.movers.Count.ShouldBe(2);

            mover = manager.movers[1];
            mover.Label.ShouldBe("test");
        }

        [Test()]
        public void CorrectSpeedFromParam()
        {
            var filename = new Filename(@"ActionRefParamChangeSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            manager.movers.Count.ShouldBe(2);

            mover = manager.movers[1];
            mover.Label.ShouldBe("test");
            mover.Speed.ShouldBe(5.0f);

            manager.Update();
            mover.Speed.ShouldBe(10.0f);
        }
    }
}

