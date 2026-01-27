using NUnit.Framework;
using FilenameBuddy;
using System;
using BulletMLLib;
using BulletMLSample;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class ParamNodeTest
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
        public void CreatedParamNode()
        {
            var filename = new Filename(@"FireRefParam.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void GotParamNode()
        {
            var filename = new Filename(@"FireRefParam.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            testFireNode.GetChild(NodeName.param).ShouldNotBeNull();
        }

        [Test()]
        public void GotParamNode1()
        {
            var filename = new Filename(@"FireRefParam.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireRefNode testFireNode = testActionNode.GetChild(NodeName.fireRef) as FireRefNode;
            (testFireNode.GetChild(NodeName.param) as ParamNode).ShouldNotBeNull();
        }

        [Test()]
        public void GotParamNode2()
        {
            var filename = new Filename(@"BulletRefParam.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletRefNode refNode = testFireNode.GetChild(NodeName.bulletRef) as BulletRefNode;
            (refNode.GetChild(NodeName.param) as ParamNode).ShouldNotBeNull();
        }

        [Test()]
        public void GotParamNode3()
        {
            var filename = new Filename(@"ActionRefParam.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testActionNode = pattern.RootNode.GetChild(NodeName.action) as ActionNode;
            FireNode testFireNode = testActionNode.GetChild(NodeName.fire) as FireNode;
            BulletNode testBulletNode = testFireNode.GetChild(NodeName.bullet) as BulletNode;
            ActionRefNode testActionRefNode = testBulletNode.GetChild(NodeName.actionRef) as ActionRefNode;
            (testActionRefNode.GetChild(NodeName.param) as ParamNode).ShouldNotBeNull();
        }
    }
}

