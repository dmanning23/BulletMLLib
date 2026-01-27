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
    public class SetSpeedTaskTest
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
        public void CorrectNode()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].Node.ShouldNotBeNull();
            (mover.Tasks[0].Node as ActionNode).ShouldNotBeNull();
        }

        [Test()]
        public void RepeatOnce()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask myAction = mover.Tasks[0] as ActionTask;

            ActionNode testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            ClassicAssert.AreEqual(1, testNode.RepeatNum(myAction, mover));
        }

        [Test()]
        public void CorrectAction()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ClassicAssert.AreEqual(1, myTask.ChildTasks.Count);
        }

        [Test()]
        public void CorrectAction1()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ClassicAssert.AreEqual(1, myTask.ChildTasks.Count);
            (myTask.ChildTasks[0] is FireTask).ShouldBeTrue();
        }

        [Test()]
        public void CorrectAction2()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.Node.ShouldNotBeNull();
            (testTask.Node.Name == NodeName.fire).ShouldBeTrue();
        }

        [Test()]
        public void NoSubTasks()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(1, testTask.ChildTasks.Count);
        }

        [Test()]
        public void FireSpeedInitInitCorrect()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialSpeedTask.ShouldNotBeNull();
        }

        [Test()]
        public void FireSpeedInitInitCorrect1()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            (testTask.InitialSpeedTask is SetSpeedTask).ShouldBeTrue();
        }

        [Test()]
        public void FireSpeedTaskValue()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

            speedTask.Node.ShouldNotBeNull();
        }

        [Test()]
        public void FireSpeedTaskValue1()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

            (speedTask.Node is SpeedNode).ShouldBeTrue();
        }

        [Test()]
        public void FireSpeedTaskValue2()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            speedNode.ShouldNotBeNull();
        }

        [Test()]
        public void FireSpeedTaskValue3()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            ClassicAssert.AreEqual(5.0f, speedNode.GetValue(speedTask, mover));
        }

        [Test()]
        public void FireSpeedInitCorrect()
        {
            var filename = new Filename(@"FireSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(5.0f, testTask.FireSpeed);
        }

        [Test()]
        public void FireSpeedInitCorrect1()
        {
            var filename = new Filename(@"FireSpeedBulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(5.0f, testTask.FireSpeed);
        }

        [Test()]
        public void BulletSpeedInitInitCorrect()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialSpeedTask.ShouldNotBeNull();
        }

        [Test()]
        public void BulletSpeedInitInitCorrect1()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            (testTask.InitialSpeedTask is SetSpeedTask).ShouldBeTrue();
        }

        [Test()]
        public void BulletSpeedTaskValue()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

            speedTask.Node.ShouldNotBeNull();
        }

        [Test()]
        public void BulletSpeedTaskValue1()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

            (speedTask.Node is SpeedNode).ShouldBeTrue();
        }

        [Test()]
        public void BulletSpeedTaskValue2()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            speedNode.ShouldNotBeNull();
        }

        [Test()]
        public void BulletSpeedTaskValue3()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
            SpeedNode speedNode = speedTask.Node as SpeedNode;

            ClassicAssert.AreEqual(10.0f, speedNode.GetValue(speedTask, mover));
        }

        [Test()]
        public void BulletSpeedInitCorrect()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            ClassicAssert.AreEqual(10.0f, testTask.FireSpeed);
        }
    }
}

