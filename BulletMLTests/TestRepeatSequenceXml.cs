using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class TestRepeatSequenceXml
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
		public void CorrectSpeed()
		{
			string filename = @"Content\RepeatSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			Assert.AreEqual(0, mover.Speed);
		}

		[Test()]
		public void CorrectSpeed1()
		{
			string filename = @"Content\RepeatSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			for (int i = 0; i < 10; i++)
			{
				manager.Update();
			}

			Assert.AreEqual(10, mover.Speed);
		}

		[Test()]
		public void CorrectSpeed2()
		{
			string filename = @"Content\RepeatSequence.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(1, mover.Speed);
		}
	}
}

