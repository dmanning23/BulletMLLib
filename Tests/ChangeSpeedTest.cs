using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using System;
using BulletMLLib;

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
            mover.Speed.ShouldBe(0);
        }

        [Test()]
        public void CorrectSpeed1()
        {
            var filename = new Filename(@"ChangeSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            mover.Speed.ShouldBe(1);
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

            mover.Speed.ShouldBe(110);
            manager.Update();
            mover.Speed.ShouldBe(100);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            mover.Speed.ShouldBe(10);
        }

        [Test()]
        public void ChangeSpeedRel()
        {
            var filename = new Filename(@"ChangeSpeedRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);

            mover.Speed.ShouldBe(100);
            manager.Update();
            mover.Speed.ShouldBe(101);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            mover.Speed.ShouldBe(110);
        }

        [Test()]
        public void ChangeSpeedSeq()
        {
            var filename = new Filename(@"ChangeSpeedSeq.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);

            mover.Speed.ShouldBe(100);
            manager.Update();
            mover.Speed.ShouldBe(110);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            mover.Speed.ShouldBe(200);
        }
    }
}

