using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class BulletMLNodeTest
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
		public void TestStringToType()
		{
			Assert.AreEqual(BulletMLNode.StringToType(""), ENodeType.none);
			Assert.AreEqual(BulletMLNode.StringToType("none"), ENodeType.none);
			Assert.AreEqual(BulletMLNode.StringToType("aim"), ENodeType.aim);
			Assert.AreEqual(BulletMLNode.StringToType("absolute"), ENodeType.absolute);
			Assert.AreEqual(BulletMLNode.StringToType("relative"), ENodeType.relative);
			Assert.AreEqual(BulletMLNode.StringToType("sequence"), ENodeType.sequence);
		}

//		[Test]
//		public void TestBadStringToType()
//		{
//			Assert.Throws(Is.InstanceOf<System.ArgumentException>(), BulletMLNode.StringToType("assnuts"));
//		}

		[Test()]
		public void TestEmpty()
		{
			var filename = new Filename(@"Empty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.AreEqual(filename.File, pattern.Filename);
			Assert.AreEqual(EPatternType.none, pattern.Orientation);

			Assert.IsNotNull(pattern.RootNode);
			Assert.AreEqual(pattern.RootNode.Name, ENodeName.bulletml);
			Assert.AreEqual(pattern.RootNode.NodeType, ENodeType.none);
		}

		[Test()]
		public void TestEmptyHoriz()
		{
			var filename = new Filename(@"EmptyHoriz.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.AreEqual(filename.File, pattern.Filename);
			Assert.AreEqual(EPatternType.horizontal, pattern.Orientation);

			Assert.IsNotNull(pattern.RootNode);
			Assert.AreEqual(pattern.RootNode.Name, ENodeName.bulletml);
			Assert.AreEqual(pattern.RootNode.NodeType, ENodeType.none);
		}

		[Test()]
		public void TestEmptyVert()
		{
			var filename = new Filename(@"EmptyVert.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.AreEqual(filename.File, pattern.Filename);
			Assert.AreEqual(EPatternType.vertical, pattern.Orientation);

			Assert.IsNotNull(pattern.RootNode);
			Assert.AreEqual(pattern.RootNode.Name, ENodeName.bulletml);
			Assert.AreEqual(pattern.RootNode.NodeType, ENodeType.none);
		}

		[Test()]
		public void TestIsParent()
		{
			var filename = new Filename(@"Empty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.AreEqual(pattern.RootNode, pattern.RootNode.GetRootNode());
		}
	}
}

