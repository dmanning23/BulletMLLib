using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class BulletRefNodeTest
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
        public void ValidXML()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void SetBulletLabelNode()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            BulletNode testBulletNode = pattern.RootNode.GetChild(NodeName.bullet) as BulletNode;
            ClassicAssert.AreEqual("test", testBulletNode.Label);
        }

        [Test()]
        public void CreatedBulletRefNode1()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.ShouldNotBeNull();
            testActionNode.GetChild(NodeName.fire).ShouldNotBeNull();
            (testActionNode.GetChild(NodeName.fire) as FireNode).ShouldNotBeNull();
        }

        [Test()]
        public void CreatedBulletRefNode2()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.GetChild(NodeName.bulletRef).ShouldNotBeNull();
        }

        [Test()]
        public void CreatedBulletRefNode3()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.GetChild(NodeName.bulletRef).ShouldNotBeNull();
            (testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode).ShouldNotBeNull();
        }

        [Test()]
        public void FoundBulletNode()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletRefNode refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            (refNode.ReferencedBulletNode).ShouldNotBeNull();
        }

        [Test()]
        public void FoundBulletNode1()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletRefNode refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            (refNode.ReferencedBulletNode as BulletNode).ShouldNotBeNull();
        }

        [Test()]
        public void FoundBulletNode2()
        {
            var filename = new Filename(@"BulletRef.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletRefNode refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            BulletNode testBulletNode = refNode.ReferencedBulletNode as BulletNode;

            ClassicAssert.AreEqual("test", testBulletNode.Label);
        }

        [Test()]
        public void FoundCorrectBulletNode()
        {
            var filename = new Filename(@"BulletRefTwoBullets.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletRefNode refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            BulletNode testBulletNode = refNode.ReferencedBulletNode as BulletNode;

            ClassicAssert.AreEqual("test2", testBulletNode.Label);
        }
    }
}

