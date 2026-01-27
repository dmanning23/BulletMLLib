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
    public class InitializeSpeedTest
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
        public void bulletCreated()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100;

            manager.Update();
            ClassicAssert.AreEqual(manager.movers.Count, 2);
        }

        [Test()]
        public void bulletDefaultSpeed()
        {
            var filename = new Filename(@"FireEmpty.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialSpeedTask.ShouldBeNull();
            testTask.SequenceSpeedTask.ShouldBeNull();
            testTask.InitialDirectionTask.ShouldBeNull();
            testTask.SequenceDirectionTask.ShouldBeNull();
        }

        [Test()]
        public void bulletDefaultSpeed1()
        {
            var filename = new Filename(@"FireEmpty.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100.0f;
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            FireTask testDude = new FireTask(testTask.Node as FireNode, testTask);
            testDude.InitialRun.ShouldBeTrue();
            testDude.InitTask(mover);

            testDude.InitialSpeedTask.ShouldBeNull();
            testDude.SequenceSpeedTask.ShouldBeNull();
            testDude.InitialDirectionTask.ShouldBeNull();
            testDude.SequenceDirectionTask.ShouldBeNull();

            ClassicAssert.AreEqual(100.0f, testDude.FireSpeed);
        }

        [Test()]
        public void bulletDefaultSpeed2()
        {
            var filename = new Filename(@"FireEmpty.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(100.0f, testDude.Speed);
        }

        [Test()]
        public void SpeedDefault()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(5.0f, testDude.Speed);
        }

        [Test()]
        public void AbsSpeedDefault()
        {
            var filename = new Filename(@"FireSpeedAbsolute.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100;
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(5.0f, testDude.Speed);
        }

        [Test()]
        public void RelSpeedDefault()
        {
            var filename = new Filename(@"FireSpeedRelative.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            Mover testDude = manager.movers[1];

            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(105.0f, testTask.FireSpeed);
        }

        [Test()]
        public void RelSpeedDefault1()
        {
            var filename = new Filename(@"FireSpeedRelative.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(105.0f, testDude.Speed);
        }

        [Test()]
        public void RightInitSpeed()
        {
            var filename = new Filename(@"FireSpeedBulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100;
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(5.0f, testDude.Speed);
        }

        [Test()]
        public void IgnoreSequenceInitSpeed()
        {
            var filename = new Filename(@"FireSpeedSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100;
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialSpeedTask.ShouldBeNull();
            testTask.SequenceSpeedTask.ShouldNotBeNull();
        }

        [Test()]
        public void IgnoreSequenceInitSpeed1()
        {
            var filename = new Filename(@"FireSpeedSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100;
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(NodeType.sequence, testTask.SequenceSpeedTask.Node.NodeType);
        }

        [Test()]
        public void IgnoreSequenceInitSpeed2()
        {
            var filename = new Filename(@"FireSpeedSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100;
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(100.0f, mover.Speed);
            testTask.InitialRun.ShouldBeFalse();
            ClassicAssert.AreEqual(5.0f, testTask.SequenceSpeedTask.Node.GetValue(testTask, mover));
        }

        [Test()]
        public void IgnoreSequenceInitSpeed23()
        {
            var filename = new Filename(@"FireSpeedSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Speed = 100;
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialSpeedTask.ShouldBeNull();
            testTask.SequenceSpeedTask.ShouldNotBeNull();
            ClassicAssert.AreEqual(NodeType.sequence, testTask.SequenceSpeedTask.Node.NodeType);
            ClassicAssert.AreEqual(100.0f, mover.Speed);
            testTask.InitialRun.ShouldBeFalse();
            ClassicAssert.AreEqual(5.0f, testTask.SequenceSpeedTask.Node.GetValue(testTask, mover));
        }

        [Test()]
        public void IgnoreSequenceInitSpeed3()
        {
            var filename = new Filename(@"FireSpeedSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(100.0f, testTask.FireSpeed);
        }

        [Test()]
        public void IgnoreSequenceInitSpeed4()
        {
            var filename = new Filename(@"FireSpeedSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(100.0f, testDude.Speed);
        }

        [Test()]
        public void FireAbsSpeed()
        {
            var filename = new Filename(@"FireSpeedAbsolute.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(5.0f, testDude.Speed);
        }

        [Test()]
        public void FireRelSpeed()
        {
            var filename = new Filename(@"FireSpeedRelative.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Speed = 100;
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            Mover testDude = manager.movers[1];

            ClassicAssert.AreEqual(105.0f, testDude.Speed);
        }

        [Test()]
        public void NestedBullets()
        {
            var filename = new Filename(@"NestedBulletsSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            //test the second bullet
            Mover testDude = manager.movers[1];
            ClassicAssert.AreEqual(10.0f, testDude.Speed);
        }

        [Test()]
        public void NestedBullets1()
        {
            var filename = new Filename(@"NestedBulletsSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            ClassicAssert.AreEqual(3, manager.movers.Count);
        }

        [Test()]
        public void NestedBullets2()
        {
            var filename = new Filename(@"NestedBulletsSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            //test the second bullet
            Mover testDude = manager.movers[2];
            ClassicAssert.AreEqual(20.0f, testDude.Speed);
        }
    }
}

