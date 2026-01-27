using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class ActionNodeTest
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
        public void TestOneTop()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            testNode.ShouldNotBeNull();
        }

        [Test()]
        public void TestNoRepeatNode()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testNode = pattern.RootNode.FindLabelNode("top", NodeName.action) as ActionNode;
            testNode.ParentRepeatNode.ShouldBeNull();
        }

        [Test()]
        public void TestManyTop()
        {
            var filename = new Filename(@"ActionManyTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ActionNode testNode = pattern.RootNode.FindLabelNode("top1", NodeName.action) as ActionNode;
            testNode.ShouldNotBeNull();
            testNode = pattern.RootNode.FindLabelNode("top2", NodeName.action) as ActionNode;
            testNode.ShouldNotBeNull();
        }
    }
}

