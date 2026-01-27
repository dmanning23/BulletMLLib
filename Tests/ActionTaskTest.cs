using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class ActionTaskTest
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
            var filename = new Filename(@"ActionOneTop.xml");
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
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
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
            var filename = new Filename(@"ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ClassicAssert.AreEqual(1, myTask.ChildTasks.Count);
        }

        [Test()]
        public void CorrectAction1()
        {
            var filename = new Filename(@"ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ClassicAssert.AreEqual(1, myTask.ChildTasks.Count);
            (myTask.ChildTasks[0] is ActionTask).ShouldBeTrue();
        }

        [Test()]
        public void CorrectAction2()
        {
            var filename = new Filename(@"ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ActionTask testTask = myTask.ChildTasks[0] as ActionTask;

            testTask.Node.ShouldNotBeNull();
            (testTask.Node.Name == NodeName.action).ShouldBeTrue();
            ClassicAssert.AreEqual(testTask.Node.Label, "test");
        }

        [Test()]
        public void RepeatNumInitCorrect()
        {
            var filename = new Filename(@"ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ActionTask testTask = myTask.ChildTasks[0] as ActionTask;
            ClassicAssert.AreEqual(0, testTask.RepeatNum);
        }

        [Test()]
        public void RepeatNumMaxInitCorrect()
        {
            var filename = new Filename(@"ActionRepeatOnce.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ActionTask testTask = myTask.ChildTasks[0] as ActionTask;
            ActionNode actionNode = testTask.Node as ActionNode;

            ClassicAssert.AreEqual(1, actionNode.RepeatNum(testTask, mover));
        }

        [Test()]
        public void RepeatNumMaxCorrect()
        {
            var filename = new Filename(@"ActionRepeatMany.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabel("test") as ActionTask;
            testTask.ShouldNotBeNull();
        }

        [Test()]
        public void RepeatNumMaxCorrect1()
        {
            var filename = new Filename(@"ActionRepeatMany.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabel("test") as ActionTask;
            ActionNode actionNode = testTask.Node as ActionNode;

            ClassicAssert.AreEqual(10, actionNode.RepeatNum(testTask, mover));
        }
    }
}

