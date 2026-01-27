using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using NUnit.Framework.Legacy;

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
            ClassicAssert.AreEqual(20, manager.movers.Count);
        }

        [Test()]
        public void CorrectSpeedFirstSet()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            ClassicAssert.AreEqual(manager.movers[0].Speed, 1);
        }

        [Test()]
        public void CorrectSpeedFirstSet1()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            ClassicAssert.AreEqual(manager.movers[1].Speed, 2);
        }

        [Test()]
        public void CorrectSpeedFirstSet2()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            ClassicAssert.AreEqual(manager.movers[0].Speed, 1);
            ClassicAssert.AreEqual(manager.movers[1].Speed, 2);
            ClassicAssert.AreEqual(manager.movers[2].Speed, 3);
            ClassicAssert.AreEqual(manager.movers[3].Speed, 4);
            ClassicAssert.AreEqual(manager.movers[4].Speed, 5);
            ClassicAssert.AreEqual(manager.movers[5].Speed, 6);
            ClassicAssert.AreEqual(manager.movers[6].Speed, 7);
            ClassicAssert.AreEqual(manager.movers[7].Speed, 8);
            ClassicAssert.AreEqual(manager.movers[8].Speed, 9);
            ClassicAssert.AreEqual(manager.movers[9].Speed, 10);
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
                ClassicAssert.AreEqual(i + 1, manager.movers[i].Speed);
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

            ClassicAssert.AreEqual(1, manager.movers[10].Speed);
        }

        [Test()]
        public void CorrectSpeedSecondSet1()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            ClassicAssert.AreEqual(2, manager.movers[11].Speed);
        }

        [Test()]
        public void CorrectSpeedAll()
        {
            var filename = new Filename(@"DoubleRepeat.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            ClassicAssert.AreEqual(manager.movers[0].Speed, 1);
            ClassicAssert.AreEqual(manager.movers[1].Speed, 2);
            ClassicAssert.AreEqual(manager.movers[2].Speed, 3);
            ClassicAssert.AreEqual(manager.movers[3].Speed, 4);
            ClassicAssert.AreEqual(manager.movers[4].Speed, 5);
            ClassicAssert.AreEqual(manager.movers[5].Speed, 6);
            ClassicAssert.AreEqual(manager.movers[6].Speed, 7);
            ClassicAssert.AreEqual(manager.movers[7].Speed, 8);
            ClassicAssert.AreEqual(manager.movers[8].Speed, 9);
            ClassicAssert.AreEqual(manager.movers[9].Speed, 10);
            ClassicAssert.AreEqual(manager.movers[10].Speed, 1);
            ClassicAssert.AreEqual(manager.movers[11].Speed, 2);
            ClassicAssert.AreEqual(manager.movers[12].Speed, 3);
            ClassicAssert.AreEqual(manager.movers[13].Speed, 4);
            ClassicAssert.AreEqual(manager.movers[14].Speed, 5);
            ClassicAssert.AreEqual(manager.movers[15].Speed, 6);
            ClassicAssert.AreEqual(manager.movers[16].Speed, 7);
            ClassicAssert.AreEqual(manager.movers[17].Speed, 8);
            ClassicAssert.AreEqual(manager.movers[18].Speed, 9);
            ClassicAssert.AreEqual(manager.movers[19].Speed, 10);
        }
    }
}

