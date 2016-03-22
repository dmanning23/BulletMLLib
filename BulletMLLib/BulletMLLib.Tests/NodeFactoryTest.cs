using NUnit.Framework;
using System;
using BulletMLLib;

namespace BulletMLTests
{
	[TestFixture()]
	public class NodeFactoryTest
	{
		[Test()]
		public void bulletTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.bullet);
			Assert.IsTrue(testNode is BulletNode);
			Assert.IsTrue(ENodeName.bullet == testNode.Name);
		}

		[Test()]
		public void actionTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.action);
			Assert.IsTrue(testNode is ActionNode);
			Assert.IsTrue(ENodeName.action == testNode.Name);
		}
		
		[Test()]
		public void fireTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.fire);
			Assert.IsTrue(testNode is FireNode);
			Assert.IsTrue(ENodeName.fire == testNode.Name);
		}
		
		[Test()]
		public void changeDirectionTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.changeDirection);
			Assert.IsTrue(testNode is ChangeDirectionNode);
			Assert.IsTrue(ENodeName.changeDirection == testNode.Name);
		}
		
		[Test()]
		public void changeSpeedTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.changeSpeed);
			Assert.IsTrue(testNode is ChangeSpeedNode);
			Assert.IsTrue(ENodeName.changeSpeed == testNode.Name);
		}
		
		[Test()]
		public void accelTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.accel);
			Assert.IsTrue(testNode is AccelNode);
			Assert.IsTrue(ENodeName.accel == testNode.Name);
		}
		
		[Test()]
		public void waitTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.wait);
			Assert.IsTrue(testNode is WaitNode);
			Assert.IsTrue(ENodeName.wait == testNode.Name);
		}
		
		[Test()]
		public void repeatTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.repeat);
			Assert.IsTrue(testNode is RepeatNode);
			Assert.IsTrue(ENodeName.repeat == testNode.Name);
		}
		
		[Test()]
		public void bulletRefTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.bulletRef);
			Assert.IsTrue(testNode is BulletRefNode);
			Assert.IsTrue(ENodeName.bulletRef == testNode.Name);
		}
		
		[Test()]
		public void actionRefTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.actionRef);
			Assert.IsTrue(testNode is ActionRefNode);
			Assert.IsTrue(ENodeName.actionRef == testNode.Name);
		}
		
		[Test()]
		public void fireRefTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.fireRef);
			Assert.IsTrue(testNode is FireRefNode);
			Assert.IsTrue(ENodeName.fireRef == testNode.Name);
		}
		
		[Test()]
		public void vanishTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.vanish);
			Assert.IsTrue(testNode is VanishNode);
			Assert.IsTrue(ENodeName.vanish == testNode.Name);
		}
		
		[Test()]
		public void horizontalTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.horizontal);
			Assert.IsTrue(testNode is HorizontalNode);
			Assert.IsTrue(ENodeName.horizontal == testNode.Name);
		}
		
		[Test()]
		public void verticalTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.vertical);
			Assert.IsTrue(testNode is VerticalNode);
			Assert.IsTrue(ENodeName.vertical == testNode.Name);
		}
		
		[Test()]
		public void termTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.term);
			Assert.IsTrue(testNode is TermNode);
			Assert.IsTrue(ENodeName.term == testNode.Name);
		}
		
		[Test()]
		public void timesTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.times);
			Assert.IsTrue(testNode is TimesNode);
			Assert.IsTrue(ENodeName.times == testNode.Name);
		}
		
		[Test()]
		public void directionTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.direction);
			Assert.IsTrue(testNode is DirectionNode);
			Assert.IsTrue(ENodeName.direction == testNode.Name);
		}
		
		[Test()]
		public void speedTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.speed);
			Assert.IsTrue(testNode is SpeedNode);
			Assert.IsTrue(ENodeName.speed == testNode.Name);
		}
		
		[Test()]
		public void paramTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.param);
			Assert.IsTrue(testNode is ParamNode);
			Assert.IsTrue(ENodeName.param == testNode.Name);
		}
		
		[Test()]
		public void bulletMLTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.bulletml);
			Assert.IsTrue(testNode is BulletMLNode);
			Assert.IsTrue(ENodeName.bulletml == testNode.Name);
		}
	}
}

