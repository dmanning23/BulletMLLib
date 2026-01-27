using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class WaitTask
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
        public void WaitOneTaskTest()
        {
            var filename = new Filename(@"WaitOne.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ClassicAssert.AreEqual(1, manager.movers.Count);
        }

        [Test()]
        public void WaitOneTaskTest1()
        {
            var filename = new Filename(@"WaitOne.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            ClassicAssert.AreEqual(1, manager.movers.Count);
        }

        [Test()]
        public void WaitOneTaskTest2()
        {
            var filename = new Filename(@"WaitOne.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            manager.Update();
            ClassicAssert.AreEqual(0, manager.movers.Count);
        }

        [Test()]
        public void WaitOneTaskTest3()
        {
            var filename = new Filename(@"Vanish.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            manager.Update();
            ClassicAssert.AreEqual(0, manager.movers.Count);
        }

        [Test()]
        public void WaitZeroTaskTest()
        {
            var filename = new Filename(@"Vanish.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            ClassicAssert.AreEqual(0, manager.movers.Count);
        }

        [Test()]
        public void WaitTwoTaskTest()
        {
            var filename = new Filename(@"WaitTwo.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ClassicAssert.AreEqual(1, manager.movers.Count);
            manager.Update();
        }

        [Test()]
        public void WaitTwoTaskTest1()
        {
            var filename = new Filename(@"WaitTwo.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            ClassicAssert.AreEqual(1, manager.movers.Count);
        }

        [Test()]
        public void WaitTwoTaskTest2()
        {
            var filename = new Filename(@"WaitTwo.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            manager.Update();
            ClassicAssert.AreEqual(1, manager.movers.Count);
        }

        [Test()]
        public void WaitTwoTaskTest3()
        {
            var filename = new Filename(@"WaitTwo.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            manager.Update();
            manager.Update();
            ClassicAssert.AreEqual(0, manager.movers.Count);
        }
    }
}

