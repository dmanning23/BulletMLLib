using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class TestDoubleRepeatXml
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
        public void CorrectNumberOfBullets()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            //there should be 20 bullets
            manager.movers.Count.ShouldBe(20);
        }

        [Test()]
        public void CorrectSpeedFirstSet()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            manager.movers[0].Speed.ShouldBe(1);
        }

        [Test()]
        public void CorrectSpeedFirstSet1()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            manager.movers[1].Speed.ShouldBe(2);
        }

        [Test()]
        public void CorrectSpeedFirstSet2()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            manager.movers[0].Speed.ShouldBe(1);
            manager.movers[1].Speed.ShouldBe(2);
            manager.movers[2].Speed.ShouldBe(3);
            manager.movers[3].Speed.ShouldBe(4);
            manager.movers[4].Speed.ShouldBe(5);
            manager.movers[5].Speed.ShouldBe(6);
            manager.movers[6].Speed.ShouldBe(7);
            manager.movers[7].Speed.ShouldBe(8);
            manager.movers[8].Speed.ShouldBe(9);
            manager.movers[9].Speed.ShouldBe(10);
        }

        [Test()]
        public void CorrectSpeedFirstSet3()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            for (int i = 0; i < 9; i++)
            {
                manager.movers[i].Speed.ShouldBe(i + 1);
            }
        }

        [Test()]
        public void CorrectSpeedSecondSet()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            manager.movers[10].Speed.ShouldBe(1);
        }

        [Test()]
        public void CorrectSpeedSecondSet1()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            manager.movers[11].Speed.ShouldBe(2);
        }

        [Test()]
        public void CorrectSpeedAll()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            manager.movers[0].Speed.ShouldBe(1);
            manager.movers[1].Speed.ShouldBe(2);
            manager.movers[2].Speed.ShouldBe(3);
            manager.movers[3].Speed.ShouldBe(4);
            manager.movers[4].Speed.ShouldBe(5);
            manager.movers[5].Speed.ShouldBe(6);
            manager.movers[6].Speed.ShouldBe(7);
            manager.movers[7].Speed.ShouldBe(8);
            manager.movers[8].Speed.ShouldBe(9);
            manager.movers[9].Speed.ShouldBe(10);
            manager.movers[10].Speed.ShouldBe(1);
            manager.movers[11].Speed.ShouldBe(2);
            manager.movers[12].Speed.ShouldBe(3);
            manager.movers[13].Speed.ShouldBe(4);
            manager.movers[14].Speed.ShouldBe(5);
            manager.movers[15].Speed.ShouldBe(6);
            manager.movers[16].Speed.ShouldBe(7);
            manager.movers[17].Speed.ShouldBe(8);
            manager.movers[18].Speed.ShouldBe(9);
            manager.movers[19].Speed.ShouldBe(10);
        }
    }
}

