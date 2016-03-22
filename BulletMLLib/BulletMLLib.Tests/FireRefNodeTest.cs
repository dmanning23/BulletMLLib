using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class FireRefNodeTest
	{
		[Test()]
		public void CreatedFireRefNode()
		{
			var filename = new Filename(@"FireRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void CreatedFireNode1()
		{
			var filename = new Filename(@"FireRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			//get teh child action node
			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode);
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fireRef));
		}

		[Test()]
		public void CreatedFireNode2()
		{
			var filename = new Filename(@"FireRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fireRef));
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fireRef) as FireRefNode);
		}

		[Test()]
		public void GotFireNode()
		{
			var filename = new Filename(@"FireRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireRefNode testFireNode = testActionNode.GetChild(ENodeName.fireRef) as FireRefNode;
			Assert.IsNotNull(testFireNode.ReferencedFireNode);
		}

		[Test()]
		public void GotFireNode1()
		{
			var filename = new Filename(@"FireRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireRefNode testFireNode = testActionNode.GetChild(ENodeName.fireRef) as FireRefNode;
			Assert.IsNotNull(testFireNode.ReferencedFireNode as FireNode);
		}

		[Test()]
		public void GotCorrectFireNode()
		{
			var filename = new Filename(@"FireRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireRefNode testFireNode = testActionNode.GetChild(ENodeName.fireRef) as FireRefNode;
			FireNode fireNode = testFireNode.ReferencedFireNode as FireNode;
			Assert.AreEqual("test", fireNode.Label);
		}

		[Test()]
		public void NoBulletNode()
		{
			var filename = new Filename(@"FireRef.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireRefNode testFireNode = testActionNode.GetChild(ENodeName.fireRef) as FireRefNode;
			Assert.IsNull(testFireNode.BulletDescriptionNode);
		}
	}
}

