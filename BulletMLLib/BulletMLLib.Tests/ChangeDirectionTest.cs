using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using Microsoft.Xna.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class ChangeDirectionTest
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
		public void ChangeDirectionAbsSetupCorrect()
		{
			var filename = new Filename(@"ChangeDirectionAbs.xml");
			pattern.ParseXML(filename.File);
			dude.pos = new Vector2(0, 100);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(0, (int)direction);
		}

		[Test()]
		public void ChangeDirectionAbs()
		{
			var filename = new Filename(@"ChangeDirectionAbs.xml");
			pattern.ParseXML(filename.File);
			dude.pos = new Vector2(0, 100);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(45, (int)direction);
		}

		[Test()]
		public void ChangeDirectionAbs1()
		{
			var filename = new Filename(@"ChangeDirectionAbs.xml");
			pattern.ParseXML(filename.File);
			dude.pos = new Vector2(0, 100);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(90, (int)direction);
		}

		[Test()]
		public void ChangeDirectionAimSetupCorrect()
		{
			var filename = new Filename(@"ChangeDirectionAim.xml");
			pattern.ParseXML(filename.File);
			dude.pos = new Vector2(100.0f, 0.0f);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(0, (int)direction);
		}

		[Test()]
		public void ChangeDirectionAim()
		{
			var filename = new Filename(@"ChangeDirectionAim.xml");
			pattern.ParseXML(filename.File);
			dude.pos = new Vector2(0.0f, 100.0f);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(45, (int)direction);
		}

		[Test()]
		public void ChangeDirectionAim1()
		{
			var filename = new Filename(@"ChangeDirectionAim.xml");
			pattern.ParseXML(filename.File);
			dude.pos = new Vector2(0.0f, 100.0f);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(90, (int)direction);
		}

		[Test()]
		public void ChangeDirectionRelSetupCorrect()
		{
			var filename = new Filename(@"ChangeDirectionRel.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(0, (int)direction);
		}

		[Test()]
		public void ChangeDirectionRel()
		{
			var filename = new Filename(@"ChangeDirectionRel.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(315, (int)direction);
		}

		[Test()]
		public void ChangeDirectionRel1()
		{
			var filename = new Filename(@"ChangeDirectionRel.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(270, (int)direction);
		}

		[Test()]
		public void ChangeDirectionSeqSetupCorrect()
		{
			var filename = new Filename(@"ChangeDirectionSeq.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(0, (int)direction);
		}

		[Test()]
		public void ChangeDirectionSeq()
		{
			var filename = new Filename(@"ChangeDirectionSeq.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(90, (int)direction);
		}

		[Test()]
		public void ChangeDirectionSeq1()
		{
			var filename = new Filename(@"ChangeDirectionSeq.xml");
			pattern.ParseXML(filename.File);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			manager.Update();
			manager.Update();
			float direction = mover.Direction * 180 / (float)Math.PI;
			Assert.AreEqual(180, (int)direction);
		}
	}
}

