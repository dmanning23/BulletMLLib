using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using System.IO;
using BulletMLLib;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class ErrorLocationTest
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
            return Should.Throw<BulletMLException>(() => pattern.ParseXML(filename.File));
        }

        [Test()]
        public void InnerExceptionIsStillInvalidData()
        {
            LoadError("ChangeSpeedNoTerm.xml").InnerException.ShouldBeOfType<InvalidDataException>();
        }

        [Test()]
        public void UnknownElementReportsNameAndLine()
        {
            var ex = LoadError("UnknownElement.xml");
            ex.InnerException.ShouldBeOfType<InvalidDataException>();
            ex.InnerException.Message.ShouldContain("Unknown element <chnageSpeed>");
            ex.LineNumber.ShouldBe(6);
        }

        [Test()]
        public void InvalidTypeReportsLine()
        {
            LoadError("SpeedTypeAim.xml").LineNumber.ShouldBe(6);
        }

        [Test()]
        public void ValidationErrorReportsLine()
        {
            var ex = LoadError("ChangeSpeedNoTerm.xml");
            ex.LineNumber.ShouldBe(5);
            ex.LinePosition.ShouldBeGreaterThan(0);
            ex.InnerException.Message.ShouldContain("<changeSpeed> requires a <term> child (line 5, column");
        }

        [Test()]
        public void MissingActionRefTargetReportsLabelAndLine()
        {
            var ex = LoadError("ActionRefMissingTarget.xml");
            ex.InnerException.ShouldBeOfType<InvalidDataException>();
            ex.InnerException.Message.ShouldContain("Couldn't find the action node \"nope\"");
            ex.LineNumber.ShouldBe(6);
        }

        [Test()]
        public void MissingBulletRefTargetReportsLabelAndLine()
        {
            var ex = LoadError("BulletRefMissingTarget.xml");
            ex.InnerException.ShouldBeOfType<InvalidDataException>();
            ex.InnerException.Message.ShouldContain("Couldn't find the bullet node \"nope\"");
            ex.LineNumber.ShouldBe(6);
        }

        [Test()]
        public void MissingFireRefTargetReportsLabelAndLine()
        {
            var ex = LoadError("FireRefMissingTarget.xml");
            ex.InnerException.ShouldBeOfType<InvalidDataException>();
            ex.InnerException.Message.ShouldContain("Couldn't find the fire node \"nope\"");
            ex.LineNumber.ShouldBe(5);
        }

        [Test()]
        public void MessageIncludesFileAndProblem()
        {
            var ex = LoadError("ChangeSpeedNoTerm.xml");
            ex.FileName.ShouldEndWith("ChangeSpeedNoTerm.xml");
            ex.Message.ShouldContain("ChangeSpeedNoTerm.xml");
            ex.Message.ShouldContain("<changeSpeed> requires a <term> child (line 5, column");
        }

        [Test()]
        public void CDataIsParsedAsText()
        {
            var filename = new Filename(@"CDataSpeed.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            manager.movers.Count.ShouldBe(2);
            manager.movers[1].Speed.ShouldBe(3.0f);
        }
    }
}
