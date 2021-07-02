using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class ActionNodeTest
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
		public void TestOneTop()
		{
			var filename = new Filename(@"ActionOneTop.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testNode = pattern.RootNode.FindLabelNode("top", ENodeName.action) as ActionNode;
			Assert.IsNotNull(testNode);
		}

		[Test()]
		public void TestNoRepeatNode()
		{
			var filename = new Filename(@"ActionOneTop.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testNode = pattern.RootNode.FindLabelNode("top", ENodeName.action) as ActionNode;
			Assert.IsNull(testNode.ParentRepeatNode);
		}

		[Test()]
		public void TestManyTop()
		{
			var filename = new Filename(@"ActionManyTop.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testNode = pattern.RootNode.FindLabelNode("top1", ENodeName.action) as ActionNode;
			Assert.IsNotNull(testNode);
			testNode = pattern.RootNode.FindLabelNode("top2", ENodeName.action) as ActionNode;
			Assert.IsNotNull(testNode);
		}
	}
}

