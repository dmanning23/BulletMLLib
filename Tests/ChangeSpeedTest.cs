using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class ChangeSpeedTest
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
        public void CorrectSpeed()
        {
            var filename = new Filename(@"ChangeSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ClassicAssert.AreEqual(0, mover.Speed);
        }

        [Test()]
        public void CorrectSpeed1()
        {
            var filename = new Filename(@"ChangeSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            ClassicAssert.AreEqual(1, mover.Speed);
        }

        [Test()]
        public void ChangeSpeedAbs()
        {
            var filename = new Filename(@"ChangeSpeedAbs.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 110;
            mover.InitTopNode(pattern.RootNode);

            ClassicAssert.AreEqual(110, mover.Speed);
            manager.Update();
            ClassicAssert.AreEqual(100, mover.Speed);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            ClassicAssert.AreEqual(10, mover.Speed);
        }

        [Test()]
        public void ChangeSpeedRel()
        {
            var filename = new Filename(@"ChangeSpeedRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);

            ClassicAssert.AreEqual(100, mover.Speed);
            manager.Update();
            ClassicAssert.AreEqual(101, mover.Speed);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            ClassicAssert.AreEqual(110, mover.Speed);
        }

        [Test()]
        public void ChangeSpeedSeq()
        {
            var filename = new Filename(@"ChangeSpeedSeq.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);

            ClassicAssert.AreEqual(100, mover.Speed);
            manager.Update();
            ClassicAssert.AreEqual(110, mover.Speed);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            ClassicAssert.AreEqual(200, mover.Speed);
        }
    }
}

