using BulletMLSample;
using FilenameBuddy;
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
			Filename.SetCurrentDirectory(@"C:\Projects\BulletMLLib\BulletMLLib\BulletMLLib.Tests\bin\Debug");
			dude = new Myship();
			manager = new MoverManager(dude.Position);
			pattern = new BulletPattern();
		}

		[Test()]
		public void CorrectNode()
		{
			var filename = new Filename(@"FireSpeed.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.IsNotNull(mover.Tasks[0].Node);
			Assert.IsNotNull(mover.Tasks[0].Node is ActionNode);
		}

		[Test()]
		public void RepeatOnce()
		{
			var filename = new Filename(@"FireSpeed.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			ActionTask myAction = mover.Tasks[0] as ActionTask;

			ActionNode testNode = pattern.RootNode.FindLabelNode("top", ENodeName.action) as ActionNode;
			Assert.AreEqual(1, testNode.RepeatNum(myAction, mover));
		}

		[Test()]
		public void CorrectAction()
		{
			var filename = new Filename(@"FireSpeed.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			Assert.AreEqual(1, myTask.ChildTasks.Count);
		}

		[Test()]
		public void CorrectAction1()
		{
			var filename = new Filename(@"FireSpeed.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			Assert.AreEqual(1, myTask.ChildTasks.Count);
			Assert.IsTrue(myTask.ChildTasks[0] is FireTask);
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

			Assert.IsNotNull(testTask.Node);
			Assert.IsTrue(testTask.Node.Name == ENodeName.fire);
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

			Assert.AreEqual(1, testTask.ChildTasks.Count);
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

			Assert.IsNotNull(testTask.InitialSpeedTask);
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

			Assert.IsTrue(testTask.InitialSpeedTask is SetSpeedTask);
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

			Assert.IsNotNull(speedTask.Node);
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

			Assert.IsTrue(speedTask.Node is SpeedNode);
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

			Assert.IsNotNull(speedNode);
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

			Assert.AreEqual(5.0f, speedNode.GetValue(speedTask, mover));
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

			Assert.AreEqual(5.0f, testTask.FireSpeed);
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

			Assert.AreEqual(5.0f, testTask.FireSpeed);
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

			Assert.IsNotNull(testTask.InitialSpeedTask);
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

			Assert.IsTrue(testTask.InitialSpeedTask is SetSpeedTask);
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

			Assert.IsNotNull(speedTask.Node);
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

			Assert.IsTrue(speedTask.Node is SpeedNode);
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

			Assert.IsNotNull(speedNode);
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

			Assert.AreEqual(10.0f, speedNode.GetValue(speedTask, mover));
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

			Assert.AreEqual(10.0f, testTask.FireSpeed);
		}
	}
}

