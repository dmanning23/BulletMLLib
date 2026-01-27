using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class BulletNodeTest
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
        public void CreatedBulletNode()
        {
            var filename = new Filename(@"BulletEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.ShouldNotBeNull();
        }

        [Test()]
        public void CreatedBulletNode1()
        {
            var filename = new Filename(@"BulletEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            BulletNode testBulletNode = pattern.RootNode.GetChild(NodeName.bullet) as BulletNode;
            testBulletNode.ShouldNotBeNull();
        }

        [Test()]
        public void SetBulletLabelNode()
        {
            var filename = new Filename(@"BulletEmpty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            BulletNode testBulletNode = pattern.RootNode.GetChild(NodeName.bullet) as BulletNode;
            ClassicAssert.AreEqual("test", testBulletNode.Label);
        }
    }
}

