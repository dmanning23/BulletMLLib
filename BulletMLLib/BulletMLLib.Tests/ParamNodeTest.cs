using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class ParamNodeTest
	{
		[Test()]
		public void CreatedParamNode()
		{
			var filename = new Filename(@"FireRefParam.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			Assert.IsNotNull(pattern.RootNode);
		}

		[Test()]
		public void GotParamNode()
		{
			var filename = new Filename(@"FireRefParam.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireRefNode testFireNode = testActionNode.GetChild(ENodeName.fireRef) as FireRefNode;
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.param));
		}

		[Test()]
		public void GotParamNode1()
		{
			var filename = new Filename(@"FireRefParam.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireRefNode testFireNode = testActionNode.GetChild(ENodeName.fireRef) as FireRefNode;
			Assert.IsNotNull(testFireNode.GetChild(ENodeName.param) as ParamNode);
		}

		[Test()]
		public void GotParamNode2()
		{
			var filename = new Filename(@"BulletRefParam.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletRefNode refNode = testFireNode.GetChild(ENodeName.bulletRef) as BulletRefNode;
			Assert.IsNotNull(refNode.GetChild(ENodeName.param) as ParamNode);
		}

		[Test()]
		public void GotParamNode3()
		{
			var filename = new Filename(@"ActionRefParam.xml");
			BulletPattern pattern = new BulletPattern();
			pattern.ParseXML(filename.File);

			ActionNode testActionNode = pattern.RootNode.GetChild(ENodeName.action) as ActionNode;
			FireNode testFireNode = testActionNode.GetChild(ENodeName.fire) as FireNode;
			BulletNode testBulletNode = testFireNode.GetChild(ENodeName.bullet) as BulletNode;
			ActionRefNode testActionRefNode = testBulletNode.GetChild(ENodeName.actionRef) as ActionRefNode;
			Assert.IsNotNull(testActionRefNode.GetChild(ENodeName.param) as ParamNode);
		}
	}
}

