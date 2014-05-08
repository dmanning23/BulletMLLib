using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

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
			string filename = @"Content\FireEmpty.xml";
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
			string filename = @"Content\FireEmpty.xml";
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
			string filename = @"Content\FireEmpty.xml";
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
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
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
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNotNull(testTask.Node);
			Assert.IsTrue(testTask.Node.Name == ENodeName.fire);
			Assert.AreEqual(testTask.Node.Label, "test1");
		}

		[Test()]
		public void NoSubTasks()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(1, testTask.ChildTasks.Count);
		}

		[Test()]
		public void NoSubTasks1()
		{
			string filename = @"Content\FireSpeedBulletSpeed.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(1, testTask.ChildTasks.Count);
		}

		[Test()]
		public void FireDirectionInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			float direction = testTask.FireDirection * 180 / (float)Math.PI;
			Assert.AreEqual(direction, 180.0f);
		}

		[Test()]
		public void FireDirectionInitCorrect1()
		{
			dude.pos.Y = -100.0f;
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			float direction = testTask.FireDirection * 180 / (float)Math.PI;
			Assert.AreEqual(direction, 0.0f);
		}

		[Test()]
		public void FireDirectionInitCorrect2()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			float direction = testTask.FireDirection * 180 / (float)Math.PI;
			Assert.AreEqual(direction, 90.0f);
		}

		[Test()]
		public void FireDirectionInitCorrect3()
		{
			dude.pos.X = -100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			float direction = testTask.FireDirection * 180 / (float)Math.PI;
			Assert.AreEqual(270.0f, direction);
		}

		[Test()]
		public void FireSpeedInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(0, testTask.FireSpeed);
		}

		[Test()]
		public void FireInitialRunInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(false, testTask.InitialRun);
		}

		[Test()]
		public void FireBulletRefInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNotNull(testTask.BulletRefTask);
		}

		[Test()]
		public void FireInitDirInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNull(testTask.InitialDirectionTask);
		}

		[Test()]
		public void FireSpeedInitInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNull(testTask.InitialSpeedTask);
		}

		[Test()]
		public void FireDirSeqInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNull(testTask.SequenceDirectionTask);
		}

		[Test()]
		public void FireSpeedSeqInitCorrect()
		{
			string filename = @"Content\FireEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNull(testTask.SequenceSpeedTask);
		}

		[Test()]
		public void FoundBullet()
		{
			string filename = @"Content\BulletSpeed.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;
			Assert.IsNotNull(testTask.BulletRefTask);
		}

		[Test()]
		public void FoundBulletNoSubTasks()
		{
			string filename = @"Content\BulletSpeed.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(1, testTask.ChildTasks.Count);
		}
	}
}

