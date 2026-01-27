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
    public class FireNodeTest
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
        public void CreatedFireNode()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedFireNode1()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            //get teh child action node
            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.ShouldNotBeNull();
            testActionNode.GetChild(NodeName.fire).ShouldNotBeNull();
        }

        [Test()]
        public void CreatedFireNode2()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.GetChild(NodeName.fire).ShouldNotBeNull();
            (testActionNode.GetChild(NodeName.fire) as FireNode).ShouldNotBeNull();
        }

        [Test()]
        public void GotBulletNode()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.BulletDescriptionNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedTopLevelFireNode()
        {
            var filename = new Filename(@"FireEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            FireNode testFireNode = pattern.RootNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.ShouldNotBeNull();
            testFireNode.BulletDescriptionNode.ShouldNotBeNull();
            ClassicAssert.AreEqual("test", testFireNode.Label);
        }
    }
}

