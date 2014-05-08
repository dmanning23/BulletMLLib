using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;
using System.IO;
using Microsoft.Xna.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class VerifyTestHarness
	{
		MoverManager manager;
		Myship dude;

		[SetUp()]
		public void setupHarness()
		{
			dude = new Myship();
			manager = new MoverManager(dude.Position);
		}

		[Test()]
		public void MakeSureNothingCrashes()
		{
			//Get all the xml files in the Content\\Samples directory
			foreach (var source in Directory.GetFiles("Content", "*.xml"))
			{
				//load & validate the pattern
				BulletPattern pattern = new BulletPattern();
				pattern.ParseXML(source);

				//fire in the hole
				manager.movers.Clear();
				Mover mover = (Mover)manager.CreateBullet();
				mover.InitTopNode(pattern.RootNode);
			}
		}

		[Test()]
		public void NoBullet()
		{
			string filename = @"Content\Empty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void NoBullet1()
		{
			string filename = @"Content\FireEmptyNoBullets.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void NoBullet2()
		{
			string filename = @"Content\BulletEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void NoBullet3()
		{
			string filename = @"Content\ActionEmpty.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void OneBullet()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(1, manager.movers.Count);
		}

		[Test()]
		public void OneBullet1()
		{
			string filename = @"Content\FireDirection.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(1, manager.movers.Count);
		}

		[Test()]
		public void TwoBullets()
		{
			string filename = @"Content\ActionManyTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateTopBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}
	}
}

