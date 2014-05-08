using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

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
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.IsNotNull(mover.Tasks[0].Node);
			Assert.IsNotNull(mover.Tasks[0].Node is ActionNode);
		}
		
		[Test()]
		public void RepeatOnce()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			ActionTask myAction = mover.Tasks[0] as ActionTask;

			ActionNode testNode = pattern.RootNode.FindLabelNode("top", ENodeName.action) as ActionNode;
			Assert.AreEqual(1, testNode.RepeatNum(myAction, mover));
		}

		[Test()]
		public void CorrectAction()
		{
			string filename = @"Content\ActionRepeatOnce.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			Assert.AreEqual(1, myTask.ChildTasks.Count);
		}

		[Test()]
		public void CorrectAction1()
		{
			string filename = @"Content\ActionRepeatOnce.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			Assert.AreEqual(1, myTask.ChildTasks.Count);
			Assert.IsTrue(myTask.ChildTasks[0] is ActionTask);
		}

		[Test()]
		public void CorrectAction2()
		{
			string filename = @"Content\ActionRepeatOnce.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			ActionTask testTask = myTask.ChildTasks[0] as ActionTask;

			Assert.IsNotNull(testTask.Node);
			Assert.IsTrue(testTask.Node.Name == ENodeName.action);
			Assert.AreEqual(testTask.Node.Label, "test");
		}

		[Test()]
		public void RepeatNumInitCorrect()
		{
			string filename = @"Content\ActionRepeatOnce.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			ActionTask testTask = myTask.ChildTasks[0] as ActionTask;
			Assert.AreEqual(0, testTask.RepeatNum);
		}

		[Test()]
		public void RepeatNumMaxInitCorrect()
		{
			string filename = @"Content\ActionRepeatOnce.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			ActionTask testTask = myTask.ChildTasks[0] as ActionTask;
			ActionNode actionNode = testTask.Node as ActionNode;

			Assert.AreEqual(1, actionNode.RepeatNum(testTask, mover));
		}

		[Test()]
		public void RepeatNumMaxCorrect()
		{
			string filename = @"Content\ActionRepeatMany.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			ActionTask testTask = mover.FindTaskByLabel("test") as ActionTask;
			Assert.IsNotNull(testTask);
		}

		[Test()]
		public void RepeatNumMaxCorrect1()
		{
			string filename = @"Content\ActionRepeatMany.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			ActionTask testTask = mover.FindTaskByLabel("test") as ActionTask;
			ActionNode actionNode = testTask.Node as ActionNode;

			Assert.AreEqual(10, actionNode.RepeatNum(testTask, mover));
		}
	}
}

