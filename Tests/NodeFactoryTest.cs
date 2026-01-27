using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;

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
            dude = new Myship();
            manager = new MoverManager(dude.Position);
        }

        [Test()]
        public void bulletTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.bullet, manager);
            (testNode is BulletNode).ShouldBeTrue();
            (NodeName.bullet == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void actionTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.action, manager);
            (testNode is ActionNode).ShouldBeTrue();
            (NodeName.action == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void fireTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.fire, manager);
            (testNode is FireNode).ShouldBeTrue();
            (NodeName.fire == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void changeDirectionTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.changeDirection, manager);
            (testNode is ChangeDirectionNode).ShouldBeTrue();
            (NodeName.changeDirection == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void changeSpeedTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.changeSpeed, manager);
            (testNode is ChangeSpeedNode).ShouldBeTrue();
            (NodeName.changeSpeed == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void accelTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.accel, manager);
            (testNode is AccelNode).ShouldBeTrue();
            (NodeName.accel == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void waitTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.wait, manager);
            (testNode is WaitNode).ShouldBeTrue();
            (NodeName.wait == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void repeatTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.repeat, manager);
            (testNode is RepeatNode).ShouldBeTrue();
            (NodeName.repeat == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void bulletRefTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.bulletRef, manager);
            (testNode is BulletRefNode).ShouldBeTrue();
            (NodeName.bulletRef == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void actionRefTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.actionRef, manager);
            (testNode is ActionRefNode).ShouldBeTrue();
            (NodeName.actionRef == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void fireRefTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.fireRef, manager);
            (testNode is FireRefNode).ShouldBeTrue();
            (NodeName.fireRef == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void vanishTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.vanish, manager);
            (testNode is VanishNode).ShouldBeTrue();
            (NodeName.vanish == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void horizontalTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.horizontal, manager);
            (testNode is HorizontalNode).ShouldBeTrue();
            (NodeName.horizontal == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void verticalTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.vertical, manager);
            (testNode is VerticalNode).ShouldBeTrue();
            (NodeName.vertical == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void termTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.term, manager);
            (testNode is TermNode).ShouldBeTrue();
            (NodeName.term == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void timesTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.times, manager);
            (testNode is TimesNode).ShouldBeTrue();
            (NodeName.times == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void directionTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.direction, manager);
            (testNode is DirectionNode).ShouldBeTrue();
            (NodeName.direction == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void speedTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.speed, manager);
            (testNode is SpeedNode).ShouldBeTrue();
            (NodeName.speed == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void paramTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.param, manager);
            (testNode is ParamNode).ShouldBeTrue();
            (NodeName.param == testNode.Name).ShouldBeTrue();
        }

        [Test()]
        public void bulletMLTestCase()
        {
            BulletMLNode testNode = NodeFactory.CreateNode(NodeName.bulletml, manager);
            (testNode is BulletMLNode).ShouldBeTrue();
            (NodeName.bulletml == testNode.Name).ShouldBeTrue();
        }
    }
}

