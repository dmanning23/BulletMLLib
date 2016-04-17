using BulletMLSample;
using FilenameBuddy;
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
			Filename.SetCurrentDirectory(@"C:\Projects\BulletMLLib\BulletMLLib\BulletMLLib.Tests\bin\Debug");
			dude = new Myship();
			manager = new MoverManager(dude.Position);
		}

		[Test()]
		public void MakeSureNothingCrashes()
		{
			//Get all the xml files in the Content\\Samples directory
			var filename = new Filename("");
			foreach (var source in Directory.GetFiles(filename.GetPath(), "*.xml"))
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
			var filename = new Filename(@"Empty.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void NoBullet1()
		{
			var filename = new Filename(@"FireEmptyNoBullets.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void NoBullet2()
		{
			var filename = new Filename(@"BulletEmpty.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void NoBullet3()
		{
			var filename = new Filename(@"ActionEmpty.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}

		[Test()]
		public void OneBullet()
		{
			var filename = new Filename(@"ActionOneTop.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(1, manager.movers.Count);
		}

		[Test()]
		public void OneBullet1()
		{
			var filename = new Filename(@"FireDirection.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(1, manager.movers.Count);
		}

		[Test()]
		public void TwoBullets()
		{
			var filename = new Filename(@"ActionManyTop.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateTopBullet();
			mover.InitTopNode(pattern.RootNode);
			manager.FreeMovers();

			Assert.AreEqual(0, manager.movers.Count);
		}
	}
}

