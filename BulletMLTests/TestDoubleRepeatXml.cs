using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class TestDoubleRepeatXml
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
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			//there should be 20 bullets
			Assert.AreEqual(20, manager.movers.Count);
		}

		[Test()]
		public void CorrectSpeedFirstSet()
		{
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(manager.movers[0].Speed, 1);
		}

		[Test()]
		public void CorrectSpeedFirstSet1()
		{
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(manager.movers[1].Speed, 2);
		}

		[Test()]
		public void CorrectSpeedFirstSet2()
		{
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(manager.movers[0].Speed, 1);
			Assert.AreEqual(manager.movers[1].Speed, 2);
			Assert.AreEqual(manager.movers[2].Speed, 3);
			Assert.AreEqual(manager.movers[3].Speed, 4);
			Assert.AreEqual(manager.movers[4].Speed, 5);
			Assert.AreEqual(manager.movers[5].Speed, 6);
			Assert.AreEqual(manager.movers[6].Speed, 7);
			Assert.AreEqual(manager.movers[7].Speed, 8);
			Assert.AreEqual(manager.movers[8].Speed, 9);
			Assert.AreEqual(manager.movers[9].Speed, 10);
		}

		[Test()]
		public void CorrectSpeedFirstSet3()
		{
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			for (int i = 0; i < 9; i++)
			{
				Assert.AreEqual(i + 1, manager.movers[i].Speed);
			}
		}

		[Test()]
		public void CorrectSpeedSecondSet()
		{
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(1, manager.movers[10].Speed);
		}

		[Test()]
		public void CorrectSpeedSecondSet1()
		{
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(2, manager.movers[11].Speed);
		}

		[Test()]
		public void CorrectSpeedAll()
		{
			string filename = @"Content\DoubleRepeat.xml";
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.Update();

			Assert.AreEqual(manager.movers[0].Speed, 1);
			Assert.AreEqual(manager.movers[1].Speed, 2);
			Assert.AreEqual(manager.movers[2].Speed, 3);
			Assert.AreEqual(manager.movers[3].Speed, 4);
			Assert.AreEqual(manager.movers[4].Speed, 5);
			Assert.AreEqual(manager.movers[5].Speed, 6);
			Assert.AreEqual(manager.movers[6].Speed, 7);
			Assert.AreEqual(manager.movers[7].Speed, 8);
			Assert.AreEqual(manager.movers[8].Speed, 9);
			Assert.AreEqual(manager.movers[9].Speed, 10);
			Assert.AreEqual(manager.movers[10].Speed, 1);
			Assert.AreEqual(manager.movers[11].Speed, 2);
			Assert.AreEqual(manager.movers[12].Speed, 3);
			Assert.AreEqual(manager.movers[13].Speed, 4);
			Assert.AreEqual(manager.movers[14].Speed, 5);
			Assert.AreEqual(manager.movers[15].Speed, 6);
			Assert.AreEqual(manager.movers[16].Speed, 7);
			Assert.AreEqual(manager.movers[17].Speed, 8);
			Assert.AreEqual(manager.movers[18].Speed, 9);
			Assert.AreEqual(manager.movers[19].Speed, 10);
		}
	}
}

