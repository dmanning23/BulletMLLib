using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using NUnit.Framework.Legacy;

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

            ClassicAssert.AreEqual(2, manager.movers.Count);

            mover = manager.movers[1];
            ClassicAssert.AreEqual("testBullet", mover.Label);
        }

        [Test()]
        public void CorrectSpeedFromParam()
        {
            var filename = new Filename(@"FireRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            ClassicAssert.AreEqual(2, manager.movers.Count);

            mover = manager.movers[1];
            ClassicAssert.AreEqual("testBullet", mover.Label);
            ClassicAssert.AreEqual(15.0f, mover.Speed);
        }
    }
}

