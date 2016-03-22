using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class BulletRefNodeTest
	{
		[Test()]
		public void ValidXML()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void SetBulletLabelNode()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			BulletNode testBulletNode = pattern.RootNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.AreEqual("test", testBulletNode.Label);
		}

		[Test()]
		public void CreatedBulletRefNode1()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode);
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fire));
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fire) as FireNode);
		}

		[Test()]
		public void CreatedBulletRefNode2()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.bulletRef));
		}

		[Test()]
		public void CreatedBulletRefNode3()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.bulletRef));
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.bulletRef) as BulletRefNode);
		}

		[Test()]
		public void FoundBulletNode()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletRefNode refNode = testFireNode.GetChild(ENodeName.bulletRef) as BulletRefNode;
			Assert.IsNotNull(refNode.ReferencedBulletNode);
		}

		[Test()]
		public void FoundBulletNode1()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletRefNode refNode = testFireNode.GetChild(ENodeName.bulletRef) as BulletRefNode;
			Assert.IsNotNull(refNode.ReferencedBulletNode as BulletNode);
		}

		[Test()]
		public void FoundBulletNode2()
		{
			var filename = new Filename(@"BulletRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletRefNode refNode = testFireNode.GetChild(ENodeName.bulletRef) as BulletRefNode;
			BulletNode testBulletNode = refNode.ReferencedBulletNode as BulletNode;

			Assert.AreEqual("test", testBulletNode.Label);
		}

		[Test()]
		public void FoundCorrectBulletNode()
		{
			var filename = new Filename(@"BulletRefTwoBullets.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletRefNode refNode = testFireNode.GetChild(ENodeName.bulletRef) as BulletRefNode;
			BulletNode testBulletNode = refNode.ReferencedBulletNode as BulletNode;

			Assert.AreEqual("test2", testBulletNode.Label);
		}
	}
}

