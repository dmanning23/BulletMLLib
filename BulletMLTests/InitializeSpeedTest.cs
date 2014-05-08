using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

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
			pattern = new BulletPattern();
		}

		[Test()]
		public void bulletCreated()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100;

			manager.Update();
			Assert.AreEqual(manager.movers.Count, 2);
		}

		[Test()]
		public void bulletDefaultSpeed()
		{
			string filename = @"Content\FireEmpty.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNull(testTask.InitialSpeedTask);
			Assert.IsNull(testTask.SequenceSpeedTask);
			Assert.IsNull(testTask.InitialDirectionTask);
			Assert.IsNull(testTask.SequenceDirectionTask);
		}

		[Test()]
		public void bulletDefaultSpeed1()
		{
			string filename = @"Content\FireEmpty.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100.0f;
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			FireTask testDude = new FireTask(testTask.Node as FireNode, testTask);
			Assert.IsTrue(testDude.InitialRun);
			testDude.InitTask(mover);

			Assert.IsNull(testDude.InitialSpeedTask);
			Assert.IsNull(testDude.SequenceSpeedTask);
			Assert.IsNull(testDude.InitialDirectionTask);
			Assert.IsNull(testDude.SequenceDirectionTask);

			Assert.AreEqual(100.0f, testDude.FireSpeed);
		}

		[Test()]
		public void bulletDefaultSpeed2()
		{
			string filename = @"Content\FireEmpty.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(100.0f, testDude.Speed);
		}

		[Test()]
		public void SpeedDefault()
		{
			string filename = @"Content\FireSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(5.0f, testDude.Speed);
		}

		[Test()]
		public void AbsSpeedDefault()
		{
			string filename = @"Content\FireSpeedAbsolute.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100;
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(5.0f, testDude.Speed);
		}

		[Test()]
		public void RelSpeedDefault()
		{
			string filename = @"Content\FireSpeedRelative.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(105.0f, testTask.FireSpeed);
		}

		[Test()]
		public void RelSpeedDefault1()
		{
			string filename = @"Content\FireSpeedRelative.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(105.0f, testDude.Speed);
		}

		[Test()]
		public void RightInitSpeed()
		{
			string filename = @"Content\FireSpeedBulletSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100;
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(5.0f, testDude.Speed);
		}

		[Test()]
		public void IgnoreSequenceInitSpeed()
		{
			string filename = @"Content\FireSpeedSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100;
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNull(testTask.InitialSpeedTask);
			Assert.IsNotNull(testTask.SequenceSpeedTask);
		}

		[Test()]
		public void IgnoreSequenceInitSpeed1()
		{
			string filename = @"Content\FireSpeedSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100;
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(ENodeType.sequence, testTask.SequenceSpeedTask.Node.NodeType);
		}

		[Test()]
		public void IgnoreSequenceInitSpeed2()
		{
			string filename = @"Content\FireSpeedSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100;
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(100.0f, mover.Speed);
			Assert.IsFalse(testTask.InitialRun);
			Assert.AreEqual(5.0f, testTask.SequenceSpeedTask.Node.GetValue(testTask, mover));
		}

		[Test()]
		public void IgnoreSequenceInitSpeed23()
		{
			string filename = @"Content\FireSpeedSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 100;
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.IsNull(testTask.InitialSpeedTask);
			Assert.IsNotNull(testTask.SequenceSpeedTask);
			Assert.AreEqual(ENodeType.sequence, testTask.SequenceSpeedTask.Node.NodeType);
			Assert.AreEqual(100.0f, mover.Speed);
			Assert.IsFalse(testTask.InitialRun);
			Assert.AreEqual(5.0f, testTask.SequenceSpeedTask.Node.GetValue(testTask, mover));
		}

		[Test()]
		public void IgnoreSequenceInitSpeed3()
		{
			string filename = @"Content\FireSpeedSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			BulletMLTask myTask = mover.Tasks[0];
			FireTask testTask = myTask.ChildTasks[0] as FireTask;

			Assert.AreEqual(100.0f, testTask.FireSpeed);
		}

		[Test()]
		public void IgnoreSequenceInitSpeed4()
		{
			string filename = @"Content\FireSpeedSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(100.0f, testDude.Speed);
		}

		[Test()]
		public void FireAbsSpeed()
		{
			string filename = @"Content\FireSpeedAbsolute.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(5.0f, testDude.Speed);
		}

		[Test()]
		public void FireRelSpeed()
		{
			string filename = @"Content\FireSpeedRelative.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			Assert.AreEqual(105.0f, testDude.Speed);
		}

		[Test()]
		public void NestedBullets()
		{
			string filename = @"Content\NestedBulletsSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			//test the second bullet
			Mover testDude = manager.movers[1];
			Assert.AreEqual(10.0f, testDude.Speed);
		}

		[Test()]
		public void NestedBullets1()
		{
			string filename = @"Content\NestedBulletsSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(3, manager.movers.Count);
		}

		[Test()]
		public void NestedBullets2()
		{
			string filename = @"Content\NestedBulletsSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			//test the second bullet
			Mover testDude = manager.movers[2];
			Assert.AreEqual(20.0f, testDude.Speed);
		}
	}
}

