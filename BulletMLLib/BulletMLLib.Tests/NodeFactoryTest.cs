using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;

namespace BulletMLTests
{
	[TestFixture()]
	public class NodeFactoryTest
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
		public void bulletTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.bullet, manager);
			Assert.IsTrue(testNode is BulletNode);
			Assert.IsTrue(ENodeName.bullet == testNode.Name);
		}

		[Test()]
		public void actionTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.action, manager);
			Assert.IsTrue(testNode is ActionNode);
			Assert.IsTrue(ENodeName.action == testNode.Name);
		}
		
		[Test()]
		public void fireTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.fire, manager);
			Assert.IsTrue(testNode is FireNode);
			Assert.IsTrue(ENodeName.fire == testNode.Name);
		}
		
		[Test()]
		public void changeDirectionTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.changeDirection, manager);
			Assert.IsTrue(testNode is ChangeDirectionNode);
			Assert.IsTrue(ENodeName.changeDirection == testNode.Name);
		}
		
		[Test()]
		public void changeSpeedTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.changeSpeed, manager);
			Assert.IsTrue(testNode is ChangeSpeedNode);
			Assert.IsTrue(ENodeName.changeSpeed == testNode.Name);
		}
		
		[Test()]
		public void accelTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.accel, manager);
			Assert.IsTrue(testNode is AccelNode);
			Assert.IsTrue(ENodeName.accel == testNode.Name);
		}
		
		[Test()]
		public void waitTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.wait, manager);
			Assert.IsTrue(testNode is WaitNode);
			Assert.IsTrue(ENodeName.wait == testNode.Name);
		}
		
		[Test()]
		public void repeatTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.repeat, manager);
			Assert.IsTrue(testNode is RepeatNode);
			Assert.IsTrue(ENodeName.repeat == testNode.Name);
		}
		
		[Test()]
		public void bulletRefTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.bulletRef, manager);
			Assert.IsTrue(testNode is BulletRefNode);
			Assert.IsTrue(ENodeName.bulletRef == testNode.Name);
		}
		
		[Test()]
		public void actionRefTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.actionRef, manager);
			Assert.IsTrue(testNode is ActionRefNode);
			Assert.IsTrue(ENodeName.actionRef == testNode.Name);
		}
		
		[Test()]
		public void fireRefTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.fireRef, manager);
			Assert.IsTrue(testNode is FireRefNode);
			Assert.IsTrue(ENodeName.fireRef == testNode.Name);
		}
		
		[Test()]
		public void vanishTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.vanish, manager);
			Assert.IsTrue(testNode is VanishNode);
			Assert.IsTrue(ENodeName.vanish == testNode.Name);
		}
		
		[Test()]
		public void horizontalTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.horizontal, manager);
			Assert.IsTrue(testNode is HorizontalNode);
			Assert.IsTrue(ENodeName.horizontal == testNode.Name);
		}
		
		[Test()]
		public void verticalTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.vertical, manager);
			Assert.IsTrue(testNode is VerticalNode);
			Assert.IsTrue(ENodeName.vertical == testNode.Name);
		}
		
		[Test()]
		public void termTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.term, manager);
			Assert.IsTrue(testNode is TermNode);
			Assert.IsTrue(ENodeName.term == testNode.Name);
		}
		
		[Test()]
		public void timesTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.times, manager);
			Assert.IsTrue(testNode is TimesNode);
			Assert.IsTrue(ENodeName.times == testNode.Name);
		}
		
		[Test()]
		public void directionTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.direction, manager);
			Assert.IsTrue(testNode is DirectionNode);
			Assert.IsTrue(ENodeName.direction == testNode.Name);
		}
		
		[Test()]
		public void speedTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.speed, manager);
			Assert.IsTrue(testNode is SpeedNode);
			Assert.IsTrue(ENodeName.speed == testNode.Name);
		}
		
		[Test()]
		public void paramTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.param, manager);
			Assert.IsTrue(testNode is ParamNode);
			Assert.IsTrue(ENodeName.param == testNode.Name);
		}
		
		[Test()]
		public void bulletMLTestCase()
		{
			BulletMLNode testNode = NodeFactory.CreateNode(ENodeName.bulletml, manager);
			Assert.IsTrue(testNode is BulletMLNode);
			Assert.IsTrue(ENodeName.bulletml == testNode.Name);
		}
	}
}

