using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using System.IO;

namespace BulletMLTests
{
    [TestFixture()]
    public class VerifyTestHarness
    {
        MoverManager manager;
        Myship dude;

        [SetUp()]
        public void setupHarness()
        {
            dude = new Myship();
            manager = new MoverManager(dude.Position);
        }

        [Test()]
        public void MakeSureNothingCrashes()
        {
            //Get all the xml files in the Content\\Samples directory
            var filename = new Filename("");
            foreach (var source in Directory.GetFiles(filename.GetPath(), "*.xml"))
            {
                //load & validate the pattern
                BulletPattern pattern = new BulletPattern(manager);
                pattern.ParseXML(source);

                //fire in the hole
                manager.movers.Clear();
                Mover mover = (Mover)manager.CreateBullet();
                mover.InitTopNode(pattern.RootNode);
            }
        }

        [Test()]
        public void NoBullet()
        {
            var filename = new Filename(@"Empty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.FreeMovers();

            manager.movers.Count.ShouldBe(0);
        }

        [Test()]
        public void NoBullet1()
        {
            var filename = new Filename(@"FireEmptyNoBullets.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.FreeMovers();

            manager.movers.Count.ShouldBe(0);
        }

        [Test()]
        public void NoBullet2()
        {
            var filename = new Filename(@"BulletEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.FreeMovers();

            manager.movers.Count.ShouldBe(0);
        }

        [Test()]
        public void NoBullet3()
        {
            var filename = new Filename(@"ActionEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.FreeMovers();

            manager.movers.Count.ShouldBe(0);
        }

        [Test()]
        public void OneBullet()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.FreeMovers();

            manager.movers.Count.ShouldBe(1);
        }

        [Test()]
        public void OneBullet1()
        {
            var filename = new Filename(@"FireDirection.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.FreeMovers();

            manager.movers.Count.ShouldBe(1);
        }

        [Test()]
        public void TwoBullets()
        {
            var filename = new Filename(@"ActionManyTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateTopBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.FreeMovers();

            manager.movers.Count.ShouldBe(0);
        }
    }
}

