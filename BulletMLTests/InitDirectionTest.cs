using BulletMLSample;
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
			dude = new Myship();
			manager = new MoverManager(dude.Position);
			pattern = new BulletPattern();
		}

		[Test()]
		public void IgnoreSequenceInitSpeed()
		{
			dude.pos.X = 100.0f;
			dude.pos.Y = 0.0f;
			string filename = @"Content\FireDirectionSequence.xml";
			pattern.ParseXML(filename);
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
			string filename = @"Content\FireDirectionAbsolute.xml";
			pattern.ParseXML(filename);
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
			string filename = @"Content\FireDirectionRelative.xml";
			pattern.ParseXML(filename);
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
			string filename = @"Content\FireDirectionAim.xml";
			pattern.ParseXML(filename);
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
			string filename = @"Content\FireDirection.xml";
			pattern.ParseXML(filename);
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
			string filename = @"Content\NestedBulletsDirection.xml";
			pattern.ParseXML(filename);
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
			string filename = @"Content\NestedBulletsDirection.xml";
			pattern.ParseXML(filename);
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
			string filename = @"Content\FireDirectionBulletDirection.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();
			Mover testDude = manager.movers[1];

			float direction = testDude.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(20.0f, direction);
		}
	}
}

