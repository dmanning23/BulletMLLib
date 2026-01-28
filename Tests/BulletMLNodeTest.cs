using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;

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
            BulletMLNode.StringToType("").ShouldBe(NodeType.none);
            BulletMLNode.StringToType("none").ShouldBe(NodeType.none);
            BulletMLNode.StringToType("aim").ShouldBe(NodeType.aim);
            BulletMLNode.StringToType("absolute").ShouldBe(NodeType.absolute);
            BulletMLNode.StringToType("relative").ShouldBe(NodeType.relative);
            BulletMLNode.StringToType("sequence").ShouldBe(NodeType.sequence);
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

            pattern.Filename.ShouldBe(filename.File);
            pattern.Orientation.ShouldBe(PatternType.none);

            pattern.RootNode.ShouldNotBeNull();
            pattern.RootNode.Name.ShouldBe(NodeName.bulletml);
            pattern.RootNode.NodeType.ShouldBe(NodeType.none);
        }

        [Test()]
        public void TestEmptyHoriz()
        {
            var filename = new Filename(@"EmptyHoriz.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.Filename.ShouldBe(filename.File);
            pattern.Orientation.ShouldBe(PatternType.horizontal);

            pattern.RootNode.ShouldNotBeNull();
            pattern.RootNode.Name.ShouldBe(NodeName.bulletml);
            pattern.RootNode.NodeType.ShouldBe(NodeType.none);
        }

        [Test()]
        public void TestEmptyVert()
        {
            var filename = new Filename(@"EmptyVert.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.Filename.ShouldBe(filename.File);
            pattern.Orientation.ShouldBe(PatternType.vertical);

            pattern.RootNode.ShouldNotBeNull();
            pattern.RootNode.Name.ShouldBe(NodeName.bulletml);
            pattern.RootNode.NodeType.ShouldBe(NodeType.none);
        }

        [Test()]
        public void TestIsParent()
        {
            var filename = new Filename(@"Empty.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);

            pattern.RootNode.GetRootNode().ShouldBe(pattern.RootNode);
        }
    }
}

