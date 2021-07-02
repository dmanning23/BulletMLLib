using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;
using BulletMLSample;

namespace BulletMLTests
{
	[TestFixture()]
	public class SpeedNodeTest
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
		public void CreatedSpeedNode()
		{
			var filename = new Filename(@"FireSpeed.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void CreatedSpeedNode1()
		{
			var filename = new Filename(@"FireSpeed.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fire));
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode);
		}

		[Test()]
		public void CreatedSpeedNode2()
		{
			var filename = new Filename(@"FireSpeed.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.speed));
		}
		
		[Test()]
		public void CreatedSpeedNode3()
		{
			var filename = new Filename(@"FireSpeed.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.speed) as SpeedNode);
		}

		[Test()]
		public void SpeedNodeDefaultValue()
		{
			var filename = new Filename(@"FireSpeed.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			SpeedNode testSpeedNode = testFireNode.GetChild(ENodeName.speed) as SpeedNode;

			Assert.AreEqual(ENodeType.absolute, testSpeedNode.NodeType);
		}

		[Test()]
		public void SpeedNodeAbsolute()
		{
			var filename = new Filename(@"FireSpeedAbsolute.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			SpeedNode testSpeedNode = testFireNode.GetChild(ENodeName.speed) as SpeedNode;

			Assert.AreEqual(ENodeType.absolute, testSpeedNode.NodeType);
		}

		[Test()]
		public void SpeedNodeSequence()
		{
			var filename = new Filename(@"FireSpeedSequence.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			SpeedNode testSpeedNode = testFireNode.GetChild(ENodeName.speed) as SpeedNode;

			Assert.AreEqual(ENodeType.sequence, testSpeedNode.NodeType);
		}

		[Test()]
		public void SpeedNodeRelative()
		{
			var filename = new Filename(@"FireSpeedRelative.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			SpeedNode testSpeedNode = testFireNode.GetChild(ENodeName.speed) as SpeedNode;

			Assert.AreEqual(ENodeType.relative, testSpeedNode.NodeType);
		}
	}
}

