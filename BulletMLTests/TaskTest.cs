using BulletMLSample;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class TaskTest
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
		public void OneAction()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(1, mover.Tasks.Count);
		}

		[Test()]
		public void OneAction1()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.IsNotNull(mover.Tasks[0] is ActionTask);
		}

		[Test()]
		public void NoChildTasks()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(mover.Tasks[0].ChildTasks.Count, 0);
		}

		[Test()]
		public void NoParams()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(mover.Tasks[0].ParamList.Count, 0);
		}

		[Test()]
		public void NoOwner()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.IsNull(mover.Tasks[0].Owner);
		}

		[Test()]
		public void CorrectNode()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.IsNotNull(mover.Tasks[0].Node);
		}

		[Test()]
		public void NotFinished()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.IsFalse(mover.Tasks[0].TaskFinished);
		}

		[Test()]
		public void OkFinished()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);

			Assert.AreEqual(ERunStatus.End, mover.Tasks[0].Run(mover));
		}

		[Test()]
		public void TaskFinishedFlag()
		{
			string filename = @"Content\ActionOneTop.xml";
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename);
			Mover mover = (Mover)manager.CreateBullet();
			mover.InitTopNode(pattern.RootNode);
			mover.Tasks[0].Run(mover);

			Assert.IsTrue(mover.Tasks[0].TaskFinished);
		}
	}
}

