using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class FireTaskTest
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
        public void CorrectNode()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].Node.ShouldNotBeNull();
            (mover.Tasks[0].Node as ActionNode).ShouldNotBeNull();
        }

        [Test()]
        public void RepeatOnce()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask myAction = mover.Tasks[0] as ActionTask;

            ActionNode testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            testNode.RepeatNum(myAction, mover).ShouldBe(1);
        }

        [Test()]
        public void CorrectAction()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            myTask.ChildTasks.Count.ShouldBe(1);
        }

        [Test()]
        public void CorrectAction1()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            myTask.ChildTasks.Count.ShouldBe(1);
            (myTask.ChildTasks[0] is FireTask).ShouldBeTrue();
        }

        [Test()]
        public void CorrectAction2()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.Node.ShouldNotBeNull();
            (testTask.Node.Name == NodeName.fire).ShouldBeTrue();
            testTask.Node.Label.ShouldBe("test1");
        }

        [Test()]
        public void NoSubTasks()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.ChildTasks.Count.ShouldBe(1);
        }

        [Test()]
        public void NoSubTasks1()
        {
            var filename = new Filename(@"FireSpeedBulletSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.ChildTasks.Count.ShouldBe(1);
        }

        [Test()]
        public void FireDirectionInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            float direction = testTask.FireDirection * 180 / (float)Math.PI;
            ((int)direction).ShouldBe(180);
        }

        [Test()]
        public void FireDirectionInitCorrect1()
        {
            dude.pos.Y = -100.0f;
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            float direction = testTask.FireDirection * 180 / (float)Math.PI;
            ((int)direction).ShouldBe(0);
        }

        [Test()]
        public void FireDirectionInitCorrect2()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            float direction = testTask.FireDirection * 180 / (float)Math.PI;
            ((int)direction).ShouldBe(90);
        }

        [Test()]
        public void FireDirectionInitCorrect3()
        {
            dude.pos.X = -100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            float direction = testTask.FireDirection * 180 / (float)Math.PI;
            ((int)direction).ShouldBe(270);
        }

        [Test()]
        public void FireSpeedInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.FireSpeed.ShouldBe(0);
        }

        [Test()]
        public void FireInitialRunInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialRun.ShouldBe(false);
        }

        [Test()]
        public void FireBulletRefInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.BulletRefTask.ShouldNotBeNull();
        }

        [Test()]
        public void FireInitDirInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialDirectionTask.ShouldBeNull();
        }

        [Test()]
        public void FireSpeedInitInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.InitialSpeedTask.ShouldBeNull();
        }

        [Test()]
        public void FireDirSeqInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.SequenceDirectionTask.ShouldBeNull();
        }

        [Test()]
        public void FireSpeedSeqInitCorrect()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.SequenceSpeedTask.ShouldBeNull();
        }

        [Test()]
        public void FoundBullet()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;
            testTask.BulletRefTask.ShouldNotBeNull();
        }

        [Test()]
        public void FoundBulletNoSubTasks()
        {
            var filename = new Filename(@"BulletSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            FireTask testTask = myTask.ChildTasks[0] as FireTask;

            testTask.ChildTasks.Count.ShouldBe(1);
        }
    }
}

