using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class BulletMLNodeTest
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
        public void TestStringToType()
        {
            ClassicAssert.AreEqual(BulletMLNode.StringToType(""), NodeType.none);
            ClassicAssert.AreEqual(BulletMLNode.StringToType("none"), NodeType.none);
            ClassicAssert.AreEqual(BulletMLNode.StringToType("aim"), NodeType.aim);
            ClassicAssert.AreEqual(BulletMLNode.StringToType("absolute"), NodeType.absolute);
            ClassicAssert.AreEqual(BulletMLNode.StringToType("relative"), NodeType.relative);
            ClassicAssert.AreEqual(BulletMLNode.StringToType("sequence"), NodeType.sequence);
        }

        //		[Test]
        //		public void TestBadStringToType()
        //		{
        //			Assert.Throws(Is.InstanceOf<System.ArgumentException>(), BulletMLNode.StringToType("assnuts"));
        //		}

        [Test()]
        public void TestEmpty()
        {
            var filename = new Filename(@"Empty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ClassicAssert.AreEqual(filename.File, pattern.Filename);
            ClassicAssert.AreEqual(PatternType.none, pattern.Orientation);

            pattern.RootNode.ShouldNotBeNull();
            ClassicAssert.AreEqual(pattern.RootNode.Name, NodeName.bulletml);
            ClassicAssert.AreEqual(pattern.RootNode.NodeType, NodeType.none);
        }

        [Test()]
        public void TestEmptyHoriz()
        {
            var filename = new Filename(@"EmptyHoriz.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ClassicAssert.AreEqual(filename.File, pattern.Filename);
            ClassicAssert.AreEqual(PatternType.horizontal, pattern.Orientation);

            pattern.RootNode.ShouldNotBeNull();
            ClassicAssert.AreEqual(pattern.RootNode.Name, NodeName.bulletml);
            ClassicAssert.AreEqual(pattern.RootNode.NodeType, NodeType.none);
        }

        [Test()]
        public void TestEmptyVert()
        {
            var filename = new Filename(@"EmptyVert.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ClassicAssert.AreEqual(filename.File, pattern.Filename);
            ClassicAssert.AreEqual(PatternType.vertical, pattern.Orientation);

            pattern.RootNode.ShouldNotBeNull();
            ClassicAssert.AreEqual(pattern.RootNode.Name, NodeName.bulletml);
            ClassicAssert.AreEqual(pattern.RootNode.NodeType, NodeType.none);
        }

        [Test()]
        public void TestIsParent()
        {
            var filename = new Filename(@"Empty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            ClassicAssert.AreEqual(pattern.RootNode, pattern.RootNode.GetRootNode());
        }
    }
}

