using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;

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
            testNode.RepeatNum(myAction, mover).ShouldBe(1);
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
            myTask.ChildTasks.Count.ShouldBe(1);
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

            testTask.Node.Name.ShouldBe(NodeName.actionRef);
        }

        [Test()]
        public void CreatedActionRefTask2()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;

            testTask.Node.Label.ShouldBe("circle");
        }

        [Test()]
        public void CreatedActionTask()
        {
            var filename = new Filename(@"AllRound.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ActionTask testTask = mover.FindTaskByLabelAndName("circle", NodeName.actionRef) as ActionTask;
            testTask.ChildTasks.Count.ShouldBe(1);
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
            testActionTask.Node.Name.ShouldBe(NodeName.action);
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
            testActionTask.Node.Label.ShouldBe("circle");
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
            manager.movers.Count.ShouldBe(21);
        }
    }
}

