using NUnit.Framework;
using BulletMLSample;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class ActionRefTest
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
			string filename = @"Content\ActionRefParamChangeSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(2, manager.movers.Count);

			mover = manager.movers[1];
			Assert.AreEqual("test", mover.Label);
		}

		[Test()]
		public void CorrectSpeedFromParam()
		{
			string filename = @"Content\ActionRefParamChangeSpeed.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();

			Assert.AreEqual(2, manager.movers.Count);

			mover = manager.movers[1];
			Assert.AreEqual("test", mover.Label);
			Assert.AreEqual(5.0f, mover.Speed);

			manager.Update();
			Assert.AreEqual(10.0f, mover.Speed);
		}
	}
}

