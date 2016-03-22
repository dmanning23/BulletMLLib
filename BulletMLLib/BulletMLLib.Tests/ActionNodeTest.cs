using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class ActionNodeTest
	{
		[Test()]
		public void TestOneTop()
		{
			var filename = new Filename(@"ActionOneTop.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testNode = pattern.RootNode.FindLabelNode("top", ENodeName.action) as ActionNode;
			Assert.IsNotNull(testNode);
		}

		[Test()]
		public void TestNoRepeatNode()
		{
			var filename = new Filename(@"ActionOneTop.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testNode = pattern.RootNode.FindLabelNode("top", ENodeName.action) as ActionNode;
			Assert.IsNull(testNode.ParentRepeatNode);
		}

		[Test()]
		public void TestManyTop()
		{
			var filename = new Filename(@"ActionManyTop.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testNode = pattern.RootNode.FindLabelNode("top1", ENodeName.action) as ActionNode;
			Assert.IsNotNull(testNode);
			testNode = pattern.RootNode.FindLabelNode("top2", ENodeName.action) as ActionNode;
			Assert.IsNotNull(testNode);
		}
	}
}

