using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class BulletRefTest
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
		public void CorrectBullets()
		{
			string filename = @"Content\BulletRef.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(2, manager.movers.Count);

			mover = manager.movers[1];
			Assert.AreEqual("test", mover.Label);
		}

		[Test()]
		public void CorrectParams()
		{
			string filename = @"Content\BulletRefParam.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			BulletMLTask bulletRefTask = mover.FindTaskByLabel("test");
			Assert.IsNotNull(bulletRefTask);
		}

		[Test()]
		public void CorrectParams1()
		{
			string filename = @"Content\BulletRefParam.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			BulletMLTask bulletRefTask = mover.FindTaskByLabelAndName("test", ENodeName.bullet);
			Assert.AreEqual(1, bulletRefTask.ParamList.Count);
		}

		[Test()]
		public void CorrectParams3()
		{
			string filename = @"Content\BulletRefParam.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			BulletMLTask bulletRefTask = mover.FindTaskByLabelAndName("test", ENodeName.bullet);
			Assert.AreEqual(15.0f, bulletRefTask.ParamList[0]);
		}

		[Test()]
		public void FireTaskCorrect()
		{
			string filename = @"Content\BulletRefParam.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			FireTask fireTask = mover.FindTaskByLabelAndName("testFire", ENodeName.fire) as FireTask;
			Assert.IsNotNull(fireTask);
		}

		[Test()]
		public void FireTaskCorrect1()
		{
			string filename = @"Content\BulletRefParam.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			FireTask fireTask = mover.FindTaskByLabelAndName("testFire", ENodeName.fire) as FireTask;
			Assert.IsNotNull(fireTask.InitialSpeedTask);
		}

		[Test()]
		public void CorrectSpeedFromParam()
		{
			string filename = @"Content\BulletRefParam.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(2, manager.movers.Count);

			mover = manager.movers[1];
			Assert.AreEqual("test", mover.Label);
			Assert.AreEqual(15.0f, mover.Speed);
		}
	}
}

