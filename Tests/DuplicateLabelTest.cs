using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System.IO;
using BulletMLLib;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class DuplicateLabelTest
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

        private BulletMLException LoadError(string file)
        {
            var filename = new Filename(@"Invalid/" + file);
            var ex = Should.Throw<BulletMLException>(() => pattern.ParseXML(filename.File));
            ex.InnerException.ShouldBeOfType<InvalidDataException>();
            return ex;
        }

        [Test()]
        public void DuplicateActionLabelIsRejected()
        {
            var ex = LoadError("DuplicateActionLabel.xml");
            ex.InnerException.Message.ShouldContain("Duplicate <action> label \"shoot\", first used on line 7");
            ex.LineNumber.ShouldBe(10);
        }

        [Test()]
        public void DuplicateBulletLabelIsRejected()
        {
            var ex = LoadError("DuplicateBulletLabel.xml");
            ex.InnerException.Message.ShouldContain("Duplicate <bullet> label \"b\", first used on line 9");
            ex.LineNumber.ShouldBe(12);
        }

        [Test()]
        public void NestedDuplicateFireLabelIsRejected()
        {
            var ex = LoadError("DuplicateNestedFireLabel.xml");
            ex.InnerException.Message.ShouldContain("Duplicate <fire> label \"f\", first used on line 5");
            ex.LineNumber.ShouldBe(9);
        }

        [Test()]
        public void SameLabelOnDifferentTypesIsAllowed()
        {
            var filename = new Filename(@"SameLabelDifferentTypes.xml");
            pattern.ParseXML(filename.File);

            var actionRef = pattern.RootNode.GetChild(NodeName.action).GetChild(NodeName.actionRef) as ActionRefNode;
            actionRef.ReferencedActionNode.Name.ShouldBe(NodeName.action);

            var bulletRef = pattern.RootNode.GetChild(NodeName.action).GetChild(NodeName.fire).GetChild(NodeName.bulletRef) as BulletRefNode;
            bulletRef.ReferencedBulletNode.Name.ShouldBe(NodeName.bullet);
        }
    }
}
