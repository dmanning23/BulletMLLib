using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;
using BulletMLSample;

namespace BulletMLTests
{
	[TestFixture()]
	public class FireNodeTest
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
		public void CreatedFireNode()
		{
			var filename = new Filename(@"FireEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void CreatedFireNode1()
		{
			var filename = new Filename(@"FireEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			//get teh child action node
			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode);
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fire));
		}

		[Test()]
		public void CreatedFireNode2()
		{
			var filename = new Filename(@"FireEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fire));
			Assert.IsNotNull(testActionNode.GetChild(ENodeName.fire)as FireNode);
		}

		[Test()]
		public void GotBulletNode()
		{
			var filename = new Filename(@"FireEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode.BulletDescriptionNode);
		}

		[Test()]
		public void CreatedTopLevelFireNode()
		{
			var filename = new Filename(@"FireEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			FireNode testFireNode = pattern.RootNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode);
			Assert.IsNotNull(testFireNode.BulletDescriptionNode);
			Assert.AreEqual("test", testFireNode.Label);
		}
	}
}

