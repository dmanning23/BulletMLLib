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
    public class FireRefNodeTest
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
        public void CreatedFireRefNode()
        {
            var filename = new Filename(@"FireRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedFireNode1()
        {
            var filename = new Filename(@"FireRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            //get teh child action node
            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.ShouldNotBeNull();
            testActionNode.GetChild(NodeName.fireRef).ShouldNotBeNull();
        }

        [Test()]
        public void CreatedFireNode2()
        {
            var filename = new Filename(@"FireRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.GetChild(NodeName.fireRef).ShouldNotBeNull();
            (testActionNode.GetChild(NodeName.fireRef) as FireRefNode).ShouldNotBeNull();
        }

        [Test()]
        public void GotFireNode()
        {
            var filename = new Filename(@"FireRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            testFireNode.ReferencedFireNode.ShouldNotBeNull();
        }

        [Test()]
        public void GotFireNode1()
        {
            var filename = new Filename(@"FireRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            (testFireNode.ReferencedFireNode as FireNode).ShouldNotBeNull();
        }

        [Test()]
        public void GotCorrectFireNode()
        {
            var filename = new Filename(@"FireRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            FireNode fireNode = testFireNode.ReferencedFireNode as FireNode;
            ClassicAssert.AreEqual("test", fireNode.Label);
        }

        [Test()]
        public void NoBulletNode()
        {
            var filename = new Filename(@"FireRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            testFireNode.BulletDescriptionNode.ShouldBeNull();
        }
    }
}

