using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class TestAimXml
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
		public void CorrectNumberOfBullets()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(1, manager.movers.Count);
		}

		[Test()]
		public void CorrectNumberOfBullets1()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			Assert.AreEqual(2, manager.movers.Count);
		}

		[Test()]
		public void CorrectNumberOfBullets2()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//run the thing ten times
			for (int i = 2; i < 12; i++)
			{
				manager.Update();
				Assert.AreEqual(i, manager.movers.Count);
			}

			//there should be 11 bullets
			Assert.AreEqual(11, manager.movers.Count);
		}

		[Test()]
		public void CorrectDirection()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//run the thing ten times
			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			for (int i = 1; i < manager.movers.Count; i++)
			{
				Mover testDude = manager.movers[i];
				float direction = testDude.Direction * 180 / (float)Math.PI;
				Assert.AreEqual(90.0f, direction);
			}
		}

		[Test()]
		public void SpeedInitializedCorrect()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//get the fire task
			FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
			Assert.IsNotNull(testTask);

			Assert.IsNotNull(testTask.InitialSpeedTask);
			Assert.IsNotNull(testTask.SequenceSpeedTask);
		}

		[Test()]
		public void SpeedInitializedCorrect1()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
			Assert.IsNotNull(testTask.InitialSpeedTask);
			Assert.IsNotNull(testTask.SequenceSpeedTask);
		}

		[Test()]
		public void SpeedInitializedCorrect2()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
			Assert.AreEqual(1.0f, testTask.InitialSpeedTask.GetNodeValue(mover));
			Assert.AreEqual(1.0f, testTask.SequenceSpeedTask.GetNodeValue(mover));
		}

		[Test()]
		public void SpeedInitializedCorrect3()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
			Assert.AreEqual(1, testTask.NumTimesInitialized);
		}

		[Test()]
		public void SpeedInitializedCorrect4()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
			Assert.AreEqual(1.0f, testTask.FireSpeed);
		}

		[Test()]
		public void CorrectSpeed()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//run the thing ten times
			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			//check the top bullet
			Mover testDude = manager.movers[0];
			Assert.AreEqual(0, testDude.Speed);

			//check the second bullet
			testDude = manager.movers[1];
			Assert.AreEqual(1, testDude.Speed);
		}

		[Test()]
		public void CorrectSpeed1()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\Aim.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//run the thing ten times
			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			//check the first bullet

			//check the second bullet

			for (int i = 1; i < manager.movers.Count; i++)
			{
				Mover testDude = manager.movers[i];
				Assert.AreEqual(i, testDude.Speed);
			}
		}
	}
}

