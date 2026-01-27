using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using BulletMLSample;
using Shouldly;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class DirectionNodeTest
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
        public void CreatedDirectionNode()
        {
            var filename = new Filename(@"FireDirection.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedDirectionNode1()
        {
            var filename = new Filename(@"FireDirection.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.GetChild(NodeName.fire).ShouldNotBeNull();
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedDirectionNode2()
        {
            var filename = new Filename(@"FireDirection.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.GetChild(NodeName.direction).ShouldNotBeNull();
            (testFireNode.GetChild(NodeName.direction) as DirectionNode).ShouldNotBeNull();
        }

        [Test()]
        public void DirectionNodeDefaultValue()
        {
            var filename = new Filename(@"FireDirection.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            DirectionNode testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            ClassicAssert.AreEqual(NodeType.aim, testDirectionNode.NodeType);
        }

        [Test()]
        public void DirectionNodeAim()
        {
            var filename = new Filename(@"FireDirectionAim.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            DirectionNode testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            ClassicAssert.AreEqual(NodeType.aim, testDirectionNode.NodeType);
        }

        [Test()]
        public void DirectionNodeAbsolute()
        {
            var filename = new Filename(@"FireDirectionAbsolute.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            DirectionNode testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            ClassicAssert.AreEqual(NodeType.absolute, testDirectionNode.NodeType);
        }

        [Test()]
        public void DirectionNodeSequence()
        {
            var filename = new Filename(@"FireDirectionSequence.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            DirectionNode testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            ClassicAssert.AreEqual(NodeType.sequence, testDirectionNode.NodeType);
        }

        [Test()]
        public void DirectionNodeRelative()
        {
            var filename = new Filename(@"FireDirectionRelative.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            DirectionNode testDirectionNode = testFireNode.GetChild(NodeName.direction) as DirectionNode;

            ClassicAssert.AreEqual(NodeType.relative, testDirectionNode.NodeType);
        }
    }
}

