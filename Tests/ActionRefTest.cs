using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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

            ClassicAssert.AreEqual(2, manager.movers.Count);

            mover = manager.movers[1];
            ClassicAssert.AreEqual("test", mover.Label);
        }

        [Test()]
        public void CorrectSpeedFromParam()
        {
            var filename = new Filename(@"ActionRefParamChangeSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            ClassicAssert.AreEqual(2, manager.movers.Count);

            mover = manager.movers[1];
            ClassicAssert.AreEqual("test", mover.Label);
            ClassicAssert.AreEqual(5.0f, mover.Speed);

            manager.Update();
            ClassicAssert.AreEqual(10.0f, mover.Speed);
        }
    }
}

