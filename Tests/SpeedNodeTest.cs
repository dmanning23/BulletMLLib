using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;
using BulletMLSample;
using Shouldly;
using NUnit.Framework.Legacy;

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
            dude = new Myship();
            manager = new MoverManager(dude.Position);
        }

        [Test()]
        public void CreatedSpeedNode()
        {
            var filename = new Filename(@"FireSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedSpeedNode1()
        {
            var filename = new Filename(@"FireSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.GetChild(NodeName.fire).ShouldNotBeNull();
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedSpeedNode2()
        {
            var filename = new Filename(@"FireSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.GetChild(NodeName.speed).ShouldNotBeNull();
        }

        [Test()]
        public void CreatedSpeedNode3()
        {
            var filename = new Filename(@"FireSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            (testFireNode.GetChild(NodeName.speed) as SpeedNode).ShouldNotBeNull();
        }

        [Test()]
        public void SpeedNodeDefaultValue()
        {
            var filename = new Filename(@"FireSpeed.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            SpeedNode testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;

            ClassicAssert.AreEqual(NodeType.absolute, testSpeedNode.NodeType);
        }

        [Test()]
        public void SpeedNodeAbsolute()
        {
            var filename = new Filename(@"FireSpeedAbsolute.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            SpeedNode testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;

            ClassicAssert.AreEqual(NodeType.absolute, testSpeedNode.NodeType);
        }

        [Test()]
        public void SpeedNodeSequence()
        {
            var filename = new Filename(@"FireSpeedSequence.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            SpeedNode testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;

            ClassicAssert.AreEqual(NodeType.sequence, testSpeedNode.NodeType);
        }

        [Test()]
        public void SpeedNodeRelative()
        {
            var filename = new Filename(@"FireSpeedRelative.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            SpeedNode testSpeedNode = testFireNode.GetChild(NodeName.speed) as SpeedNode;

            ClassicAssert.AreEqual(NodeType.relative, testSpeedNode.NodeType);
        }
    }
}

