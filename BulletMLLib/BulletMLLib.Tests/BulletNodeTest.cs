using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class BulletNodeTest
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
		public void CreatedBulletNode()
		{
			var filename = new Filename(@"BulletEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void CreatedBulletNode1()
		{
			var filename = new Filename(@"BulletEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			BulletNode testBulletNode = pattern.RootNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.IsNotNull(testBulletNode);
		}

		[Test()]
		public void SetBulletLabelNode()
		{
			var filename = new Filename(@"BulletEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			BulletNode testBulletNode = pattern.RootNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.AreEqual("test", testBulletNode.Label);
		}
	}
}

