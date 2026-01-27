using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using NUnit.Framework.Legacy;

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
            dude = new Myship();
            manager = new MoverManager(dude.Position);
        }

        [Test()]
        public void ValidXML()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void GotActionRefNode()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            testActionNode.ShouldNotBeNull();
        }

        [Test()]
        public void GotActionRefNode1()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            testFireNode.ShouldNotBeNull();
        }

        [Test()]
        public void GotActionRefNode2()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            testBulletNode.ShouldNotBeNull();
        }

        [Test()]
        public void GotActionRefNode3()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            testBulletNode.GetChild(NodeName.actionRef).ShouldNotBeNull();
        }

        [Test()]
        public void GotActionRefNode4()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            (testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode).ShouldNotBeNull();
        }

        [Test()]
        public void FoundActionNode()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            ActionRefNode testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;

            testActionRefNode.ReferencedActionNode.ShouldNotBeNull();
        }

        [Test()]
        public void FoundActionNode1()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            ActionRefNode testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;

            (testActionRefNode.ReferencedActionNode as ActionNode).ShouldNotBeNull();
        }

        [Test()]
        public void FoundActionNode2()
        {
            var filename = new Filename(@"ActionRefEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            ActionRefNode testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            ActionNode refNode = testActionRefNode.ReferencedActionNode as ActionNode;

            ClassicAssert.AreEqual(refNode.Label, "test");
        }

        [Test()]
        public void FoundCorrectActionNode()
        {
            var filename = new Filename(@"ActionRefParam.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            ActionRefNode testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            ActionNode refNode = testActionRefNode.ReferencedActionNode as ActionNode;

            ClassicAssert.AreEqual(refNode.Label, "test2");
        }
    }
}
