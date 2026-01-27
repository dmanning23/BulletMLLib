using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class AllRoundXmlTest
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
        public void TestOneTop()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);

            ActionNode testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            testNode.ShouldNotBeNull();
        }

        [Test()]
        public void TestNoRepeatNode()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);

            ActionNode testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            testNode.ParentRepeatNode.ShouldBeNull();
        }

        [Test()]
        public void CorrectNode()
        {
            var filename = new Filename(@"AllRound.xml");
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
            var filename = new Filename(@"AllRound.xml");
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
            var filename = new Filename(@"AllRound.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            BulletMLTask myTask = mover.Tasks[0];
            ClassicAssert.AreEqual(1, myTask.ChildTasks.Count);
        }

        [Test()]
        public void CreatedActionRefTask()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            testTask.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedActionRefTask1()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;

            ClassicAssert.AreEqual(NodeName.actionRef, testTask.Node.Name);
        }

        [Test()]
        public void CreatedActionRefTask2()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;

            ClassicAssert.AreEqual("circle", testTask.Node.Label);
        }

        [Test()]
        public void CreatedActionTask()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ClassicAssert.AreEqual(1, testTask.ChildTasks.Count);
        }

        [Test()]
        public void CreatedActionTask1()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            testTask.ChildTasks[0].ShouldNotBeNull();
        }

        [Test()]
        public void CreatedActionTask2()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            testActionTask.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedActionTask3()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            testActionTask.Node.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedActionTask4()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            ClassicAssert.AreEqual(NodeName.action, testActionTask.Node.Name);
        }

        [Test()]
        public void CreatedActionTask5()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            ActionTask testActionTask = testTask.ChildTasks[0] as ActionTask;
            ClassicAssert.AreEqual("circle", testActionTask.Node.Label);
        }

        [Test()]
        public void CreatedActionTask10()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.action) as ActionTask;
            testTask.ShouldNotBeNull();
        }

        [Test()]
        public void CorrectNumberOfBullets()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            //there should be 11 bullets
            ClassicAssert.AreEqual(21, manager.movers.Count);
        }
    }
}

