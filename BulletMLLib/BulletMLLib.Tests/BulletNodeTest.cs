using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class BulletNodeTest
	{
		[Test()]
		public void CreatedBulletNode()
		{
			var filename = new Filename(@"BulletEmpty.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void CreatedBulletNode1()
		{
			var filename = new Filename(@"BulletEmpty.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			BulletNode testBulletNode = pattern.RootNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.IsNotNull(testBulletNode);
		}

		[Test()]
		public void SetBulletLabelNode()
		{
			var filename = new Filename(@"BulletEmpty.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			BulletNode testBulletNode = pattern.RootNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.AreEqual("test", testBulletNode.Label);
		}
	}
}

