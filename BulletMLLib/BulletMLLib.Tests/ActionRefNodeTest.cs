using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class ActionRefNodeTest
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
		public void ValidXML()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void GotActionRefNode()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			Assert.IsNotNull(testActionNode);
		}

		[Test()]
		public void GotActionRefNode1()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			Assert.IsNotNull(testFireNode);
		}

		[Test()]
		public void GotActionRefNode2()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.IsNotNull(testBulletNode);
		}

		[Test()]
		public void GotActionRefNode3()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.IsNotNull(testBulletNode.GetChild(ENodeName.actionRef));
		}

		[Test()]
		public void GotActionRefNode4()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			Assert.IsNotNull(testBulletNode.GetChild(ENodeName.actionRef) as ActionRefNode);
		}

		[Test()]
		public void FoundActionNode()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			ActionRefNode testActionRefNode = testBulletNode.GetChild(ENodeName.actionRef) as ActionRefNode;

			Assert.IsNotNull(testActionRefNode.ReferencedActionNode);
		}

		[Test()]
		public void FoundActionNode1()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			ActionRefNode testActionRefNode = testBulletNode.GetChild(ENodeName.actionRef) as ActionRefNode;

			Assert.IsNotNull(testActionRefNode.ReferencedActionNode as ActionNode);
		}

		[Test()]
		public void FoundActionNode2()
		{
			var filename = new Filename(@"ActionRefEmpty.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			ActionRefNode testActionRefNode = testBulletNode.GetChild(ENodeName.actionRef) as ActionRefNode;
			ActionNode refNode = testActionRefNode.ReferencedActionNode as ActionNode;

			Assert.AreEqual(refNode.Label, "test");
		}

		[Test()]
		public void FoundCorrectActionNode()
		{
			var filename = new Filename(@"ActionRefParam.xml");
			BulletPattern pattern = new BulletPattern(manager);
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			ActionRefNode testActionRefNode = testBulletNode.GetChild(ENodeName.actionRef) as ActionRefNode;
			ActionNode refNode = testActionRefNode.ReferencedActionNode as ActionNode;

			Assert.AreEqual(refNode.Label, "test2");
		}
	}
}
