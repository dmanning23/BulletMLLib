using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class ChangeSpeedTest
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
		public void CorrectSpeed()
		{
			var filename = new Filename(@"ChangeSpeed.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			Assert.AreEqual(0, mover.Speed);
		}

		[Test()]
		public void CorrectSpeed1()
		{
			var filename = new Filename(@"ChangeSpeed.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(1, mover.Speed);
		}

		[Test()]
		public void ChangeSpeedAbs()
		{
			var filename = new Filename(@"ChangeSpeedAbs.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Speed = 110;
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(110, mover.Speed);
			manager.Update();
			Assert.AreEqual(100, mover.Speed);

			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			Assert.AreEqual(10, mover.Speed);
		}

		[Test()]
		public void ChangeSpeedRel()
		{
			var filename = new Filename(@"ChangeSpeedRel.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(100, mover.Speed);
			manager.Update();
			Assert.AreEqual(101, mover.Speed);

			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			Assert.AreEqual(110, mover.Speed);
		}

		[Test()]
		public void ChangeSpeedSeq()
		{
			var filename = new Filename(@"ChangeSpeedSeq.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Speed = 100;
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(100, mover.Speed);
			manager.Update();
			Assert.AreEqual(110, mover.Speed);

			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			Assert.AreEqual(200, mover.Speed);
		}
	}
}

