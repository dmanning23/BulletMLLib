using BulletMLSample;
using FilenameBuddy;
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
			Filename.SetCurrentDirectory(@"Y:\Documents\Projects\BulletMLLib\BulletMLLib\BulletMLLib.Tests\bin\Debug");
			dude = new Myship();
			manager = new MoverManager(dude.Position);
			pattern = new BulletPattern();
		}

		[Test()]
		public void CorrectBullets()
		{
			var filename = new Filename(@"BulletRef.xml");
			pattern.ParseXML(filename.File);
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
			var filename = new Filename(@"BulletRefParam.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			BulletMLTask bulletRefTask = mover.FindTaskByLabel("test");
			Assert.IsNotNull(bulletRefTask);
		}

		[Test()]
		public void CorrectParams1()
		{
			var filename = new Filename(@"BulletRefParam.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			BulletMLTask bulletRefTask = mover.FindTaskByLabelAndName("test", ENodeName.bullet);
			Assert.AreEqual(1, bulletRefTask.ParamList.Count);
		}

		[Test()]
		public void CorrectParams3()
		{
			var filename = new Filename(@"BulletRefParam.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			BulletMLTask bulletRefTask = mover.FindTaskByLabelAndName("test", ENodeName.bullet);
			Assert.AreEqual(15.0f, bulletRefTask.ParamList[0]);
		}

		[Test()]
		public void FireTaskCorrect()
		{
			var filename = new Filename(@"BulletRefParam.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			FireTask fireTask = mover.FindTaskByLabelAndName("testFire", ENodeName.fire) as FireTask;
			Assert.IsNotNull(fireTask);
		}

		[Test()]
		public void FireTaskCorrect1()
		{
			var filename = new Filename(@"BulletRefParam.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			//find the task for the bulletRef
			FireTask fireTask = mover.FindTaskByLabelAndName("testFire", ENodeName.fire) as FireTask;
			Assert.IsNotNull(fireTask.InitialSpeedTask);
		}

		[Test()]
		public void CorrectSpeedFromParam()
		{
			var filename = new Filename(@"BulletRefParam.xml");
			pattern.ParseXML(filename.File);
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

