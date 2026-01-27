using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using Shouldly;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class BulletRefTest
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
            var filename = new Filename(@"BulletRef.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            ClassicAssert.AreEqual(2, manager.movers.Count);

            mover = manager.movers[1];
            ClassicAssert.AreEqual("test", mover.Label);
        }

        [Test()]
        public void CorrectParams()
        {
            var filename = new Filename(@"BulletRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //find the task for the bulletRef
            BulletMLTask bulletRefTask = mover.FindTaskByLabel("test");
            bulletRefTask.ShouldNotBeNull();
        }

        [Test()]
        public void CorrectParams1()
        {
            var filename = new Filename(@"BulletRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //find the task for the bulletRef
            BulletMLTask bulletRefTask = mover.FindTaskByLabelAndName("test", NodeName.bullet);
            ClassicAssert.AreEqual(1, bulletRefTask.ParamList.Count);
        }

        [Test()]
        public void CorrectParams3()
        {
            var filename = new Filename(@"BulletRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //find the task for the bulletRef
            BulletMLTask bulletRefTask = mover.FindTaskByLabelAndName("test", NodeName.bullet);
            ClassicAssert.AreEqual(15.0f, bulletRefTask.ParamList[0]);
        }

        [Test()]
        public void FireTaskCorrect()
        {
            var filename = new Filename(@"BulletRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //find the task for the bulletRef
            FireTask fireTask = mover.FindTaskByLabelAndName("testFire", NodeName.fire) as FireTask;
            fireTask.ShouldNotBeNull();
        }

        [Test()]
        public void FireTaskCorrect1()
        {
            var filename = new Filename(@"BulletRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //find the task for the bulletRef
            FireTask fireTask = mover.FindTaskByLabelAndName("testFire", NodeName.fire) as FireTask;
            fireTask.InitialSpeedTask.ShouldNotBeNull();
        }

        [Test()]
        public void CorrectSpeedFromParam()
        {
            var filename = new Filename(@"BulletRefParam.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            ClassicAssert.AreEqual(2, manager.movers.Count);

            mover = manager.movers[1];
            ClassicAssert.AreEqual("test", mover.Label);
            ClassicAssert.AreEqual(15.0f, mover.Speed);
        }
    }
}

