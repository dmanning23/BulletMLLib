using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class MissingLabelTest
    {
        MoverManager manager;
        Myship dude;
        BulletPattern pattern;

        [SetUp()]
        public void setupHarness()
        {
            dude = new Myship();
            manager = new MoverManager(dude.Position);
            pattern = new BulletPattern(manager);
        }

        [Test()]
        public void FindLabelNodeIgnoresUnlabeledNodes()
        {
            var filename = new Filename(@"ActionUnlabeled.xml");
            pattern.ParseXML(filename.File);

            pattern.RootNode.FindLabelNode(null, NodeName.action).ShouldBeNull();
        }

        [Test()]
        public void ActionRefWithoutLabelThrows()
        {
            var filename = new Filename(@"Invalid/ActionRefNoLabel.xml");
            var ex = Should.Throw<Exception>(() => pattern.ParseXML(filename.File));
            ex.InnerException.Message.ShouldContain("missing a label");
        }

        [Test()]
        public void BulletRefWithoutLabelThrows()
        {
            var filename = new Filename(@"Invalid/BulletRefNoLabel.xml");
            var ex = Should.Throw<Exception>(() => pattern.ParseXML(filename.File));
            ex.InnerException.Message.ShouldContain("missing a label");
        }

        [Test()]
        public void FireRefWithoutLabelThrows()
        {
            var filename = new Filename(@"Invalid/FireRefNoLabel.xml");
            var ex = Should.Throw<Exception>(() => pattern.ParseXML(filename.File));
            ex.InnerException.Message.ShouldContain("missing a label");
        }
    }
}
