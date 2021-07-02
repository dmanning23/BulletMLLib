using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class InitDirectionTest
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
			pattern = new BulletPattern(manager);
		}

		[Test()]
		public void IgnoreSequenceInitSpeed()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			var filename = new Filename(@"FireDirectionSequence.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(90.0f, direction);
		}

		[Test()]
		public void FireAbsDirection()
		{
			var filename = new Filename(@"FireDirectionAbsolute.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(10.0f, direction);
		}

		[Test()]
		public void FireRelDirection()
		{
			var filename = new Filename(@"FireDirectionRelative.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.Direction = 100.0f * (float)Math.PI / 180.0f;
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(110, (int)(direction + 0.5f));
		}

		[Test()]
		public void FireAimDirection()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			var filename = new Filename(@"FireDirectionAim.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(direction, 90.0f);
		}

		[Test()]
		public void FireDefaultDirection()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			var filename = new Filename(@"FireDirection.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(10.0f, direction);
		}

		[Test()]
		public void NestedBulletsDirection()
		{
			var filename = new Filename(@"NestedBulletsDirection.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Mover testDude = manager.movers[1];
			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(10.0f, direction);
		}

		[Test()]
		public void NestedBulletsDirection1()
		{
			var filename = new Filename(@"NestedBulletsDirection.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Mover testDude = manager.movers[2];
			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(20.0f, direction);
		}

		[Test()]
		public void InitDirectionWithSequence()
		{
			dude.pos.X = 0.0f;
			dude.pos.Y = -100.0f;
			var filename = new Filename(@"FireDirectionBulletDirection.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(20.0f, direction);
		}
	}
}

