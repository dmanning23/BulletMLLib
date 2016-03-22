using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class DirectionNodeTest
	{
		[Test()]
		public void CreatedDirectionNode()
		{
			var filename = new Filename(@"FireDirection.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void CreatedDirectionNode1()
		{
			var filename = new Filename(@"FireDirection.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fire));
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode);
		}

		[Test()]
		public void CreatedDirectionNode2()
		{
			var filename = new Filename(@"FireDirection.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.direction));
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.direction) as DirectionNode);
		}

		[Test()]
		public void DirectionNodeDefaultValue()
		{
			var filename = new Filename(@"FireDirection.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			DirectionNode testDirectionNode = testFireNode.GetChild(ENodeName.direction) as DirectionNode;

			Assert.AreEqual(ENodeType.aim, testDirectionNode.NodeType);
		}

		[Test()]
		public void DirectionNodeAim()
		{
			var filename = new Filename(@"FireDirectionAim.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			DirectionNode testDirectionNode = testFireNode.GetChild(ENodeName.direction) as DirectionNode;

			Assert.AreEqual(ENodeType.aim, testDirectionNode.NodeType);
		}

		[Test()]
		public void DirectionNodeAbsolute()
		{
			var filename = new Filename(@"FireDirectionAbsolute.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			DirectionNode testDirectionNode = testFireNode.GetChild(ENodeName.direction) as DirectionNode;

			Assert.AreEqual(ENodeType.absolute, testDirectionNode.NodeType);
		}

		[Test()]
		public void DirectionNodeSequence()
		{
			var filename = new Filename(@"FireDirectionSequence.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			DirectionNode testDirectionNode = testFireNode.GetChild(ENodeName.direction) as DirectionNode;

			Assert.AreEqual(ENodeType.sequence, testDirectionNode.NodeType);
		}

		[Test()]
		public void DirectionNodeRelative()
		{
			var filename = new Filename(@"FireDirectionRelative.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			DirectionNode testDirectionNode = testFireNode.GetChild(ENodeName.direction) as DirectionNode;

			Assert.AreEqual(ENodeType.relative, testDirectionNode.NodeType);
		}
	}
}

