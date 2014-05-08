using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;
using Microsoft.Xna.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class AccelTest
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
		public void CorrectSpeedAbs()
		{
			string filename = @"Content\AccelAbs.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);
			Assert.AreEqual(20.0f, mover.Acceleration.X);
			Assert.AreEqual(40.0f, mover.Acceleration.Y);
		}

		[Test()]
		public void CorrectSpeedAbs1()
		{
			string filename = @"Content\AccelAbs.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(19.0f, mover.Acceleration.X);
			Assert.AreEqual(38.0f, mover.Acceleration.Y);
		}

		[Test()]
		public void CorrectSpeedAbs2()
		{
			string filename = @"Content\AccelAbs.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			Assert.AreEqual(10.0f, mover.Acceleration.X);
			Assert.AreEqual(20.0f, mover.Acceleration.Y);
		}

		[Test()]
		public void CorrectSpeedRel()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(21.0f, mover.Acceleration.X);
			Assert.AreEqual(42.0f, mover.Acceleration.Y);
		}

		[Test()]
		public void CorrectSpeedRel1()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			Assert.AreEqual(30.0f, mover.Acceleration.X);
			Assert.AreEqual(60.0f, mover.Acceleration.Y);
		}
		
		[Test()]
		public void CorrectSpeedRel2()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			BulletMLTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel);
			Assert.IsNotNull(myTask);
		}

		[Test()]
		public void CorrectSpeedRel3()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			Assert.IsNotNull(myTask);
		}

		[Test()]
		public void CorrectSpeedRel4()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			Assert.AreEqual(1.0f, myTask.Acceleration.X);
			Assert.AreEqual(2.0f, myTask.Acceleration.Y);
		}

		[Test()]
		public void CorrectSpeedRel5()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			BulletMLNode myNode = myTask.Node.GetChild(ENodeName.horizontal);
			Assert.AreEqual(10.0f, myNode.GetValue(myTask, mover));
		}

		[Test()]
		public void CorrectSpeedRel6()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			BulletMLNode myNode = myTask.Node.GetChild(ENodeName.vertical);
			Assert.AreEqual(ENodeType.relative, myNode.NodeType);
		}

		[Test()]
		public void CorrectSpeedRel7()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			BulletMLNode myNode = myTask.Node.GetChild(ENodeName.horizontal);
			Assert.AreEqual(ENodeType.relative, myNode.NodeType);
		}

		[Test()]
		public void CorrectSpeedRel8()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			BulletMLNode myNode = myTask.Node.GetChild(ENodeName.vertical);
			Assert.AreEqual(20.0f, myNode.GetValue(myTask, mover));
		}

		[Test()]
		public void CorrectSpeedRel9()
		{
			string filename = @"Content\AccelRel.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			Assert.AreEqual(10.0f, myTask.Duration);
		}

		[Test()]
		public void CorrectSpeedSeq()
		{
			string filename = @"Content\AccelSeq.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			AccelTask myTask = mover.FindTaskByLabelAndName("test", ENodeName.accel) as AccelTask;
			Assert.AreEqual(1.0f, myTask.Acceleration.X);
			Assert.AreEqual(2.0f, myTask.Acceleration.Y);
		}

		[Test()]
		public void CorrectSpeedSeq1()
		{
			string filename = @"Content\AccelSeq.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(21.0f, mover.Acceleration.X);
			Assert.AreEqual(42.0f, mover.Acceleration.Y);
		}

		[Test()]
		public void CorrectSpeedSeq2()
		{
			string filename = @"Content\AccelSeq.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Acceleration = new Vector2(20.0f, 40.0f);
			mover.InitTopNode(pattern.RootNode);

			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			Assert.AreEqual(30.0f, mover.Acceleration.X);
			Assert.AreEqual(60.0f, mover.Acceleration.Y);
		}
	}
}

