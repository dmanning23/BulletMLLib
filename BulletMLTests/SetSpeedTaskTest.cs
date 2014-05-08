using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

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
			pattern = new BulletPattern();
		}

		[Test()]
		public void CorrectNode()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.IsNotNull(mover.Tasks[0].Node);
			Assert.IsNotNull(mover.Tasks[0].Node is ActionNode);
		}

		[Test()]
		public void RepeatOnce()
		{
			string filename = @"Content\FireSpeed.xml";
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
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			Assert.AreEqual(1, myTask.ChildTasks.Count);
		}

		[Test()]
		public void CorrectAction1()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			Assert.AreEqual(1, myTask.ChildTasks.Count);
			Assert.IsTrue(myTask.ChildTasks[0] is FireTask);
		}

		[Test()]
		public void CorrectAction2()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNotNull(testTask.Node);
			Assert.IsTrue(testTask.Node.Name == ENodeName.fire);
		}

		[Test()]
		public void NoSubTasks()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(1, testTask.ChildTasks.Count);
		}

		[Test()]
		public void FireSpeedInitInitCorrect()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNotNull(testTask.InitialSpeedTask);
		}

		[Test()]
		public void FireSpeedInitInitCorrect1()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsTrue(testTask.InitialSpeedTask is SetSpeedTask);
		}

		[Test()]
		public void FireSpeedTaskValue()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

			Assert.IsNotNull(speedTask.Node);
		}

		[Test()]
		public void FireSpeedTaskValue1()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

			Assert.IsTrue(speedTask.Node is SpeedNode);
		}

		[Test()]
		public void FireSpeedTaskValue2()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
			SpeedNode speedNode = speedTask.Node as SpeedNode;

			Assert.IsNotNull(speedNode);
		}

		[Test()]
		public void FireSpeedTaskValue3()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
			SpeedNode speedNode = speedTask.Node as SpeedNode;

			Assert.AreEqual(5.0f, speedNode.GetValue(speedTask, mover));
		}

		[Test()]
		public void FireSpeedInitCorrect()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(5.0f, testTask.FireSpeed);
		}

		[Test()]
		public void FireSpeedInitCorrect1()
		{
			string filename = @"Content\FireSpeedBulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(5.0f, testTask.FireSpeed);
		}

		[Test()]
		public void BulletSpeedInitInitCorrect()
		{
			string filename = @"Content\BulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNotNull(testTask.InitialSpeedTask);
		}

		[Test()]
		public void BulletSpeedInitInitCorrect1()
		{
			string filename = @"Content\BulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsTrue(testTask.InitialSpeedTask is SetSpeedTask);
		}

		[Test()]
		public void BulletSpeedTaskValue()
		{
			string filename = @"Content\BulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

			Assert.IsNotNull(speedTask.Node);
		}

		[Test()]
		public void BulletSpeedTaskValue1()
		{
			string filename = @"Content\BulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;

			Assert.IsTrue(speedTask.Node is SpeedNode);
		}

		[Test()]
		public void BulletSpeedTaskValue2()
		{
			string filename = @"Content\BulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
			SpeedNode speedNode = speedTask.Node as SpeedNode;

			Assert.IsNotNull(speedNode);
		}

		[Test()]
		public void BulletSpeedTaskValue3()
		{
			string filename = @"Content\BulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			SetSpeedTask speedTask = testTask.InitialSpeedTask as SetSpeedTask;
			SpeedNode speedNode = speedTask.Node as SpeedNode;

			Assert.AreEqual(10.0f, speedNode.GetValue(speedTask, mover));
		}

		[Test()]
		public void BulletSpeedInitCorrect()
		{
			string filename = @"Content\BulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(10.0f, testTask.FireSpeed);
		}
	}
}

