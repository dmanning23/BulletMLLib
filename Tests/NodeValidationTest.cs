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
    public class NodeValidationTest
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

        private void ShouldFailValidation(string file, string expectedMessage)
        {
            var filename = new Filename(@"Invalid/" + file);
            var ex = Should.Throw<Exception>(() => pattern.ParseXML(filename.File));
            ex.InnerException.ShouldBeOfType<InvalidDataException>();
            ex.InnerException.Message.ShouldContain(expectedMessage);
        }

        [Test()]
        public void HorizontalAndVerticalDefaultToAbsolute()
        {
            var filename = new Filename(@"AccelNoType.xml");
            pattern.ParseXML(filename.File);

            BulletMLNode accel = pattern.RootNode.GetChild(NodeName.action).GetChild(NodeName.accel);
            accel.GetChild(NodeName.horizontal).NodeType.ShouldBe(NodeType.absolute);
            accel.GetChild(NodeName.vertical).NodeType.ShouldBe(NodeType.absolute);
        }

        [Test()]
        public void SpeedRejectsAimType()
        {
            ShouldFailValidation("SpeedTypeAim.xml", "\"aim\" is not a valid type for a <speed> node");
        }

        [Test()]
        public void MisspelledTypeIsRejected()
        {
            ShouldFailValidation("DirectionTypeMisspelled.xml", "\"Absolute\" is not a valid type for a <direction> node");
        }

        [Test()]
        public void TypeOnNodeWithoutTypesIsRejected()
        {
            ShouldFailValidation("WaitWithType.xml", "\"relative\" is not a valid type for a <wait> node");
        }

        [Test()]
        public void MisplacedChildIsRejected()
        {
            ShouldFailValidation("SpeedInsideAction.xml", "<speed> is not allowed inside <action>");
        }

        [Test()]
        public void ChangeSpeedRequiresTerm()
        {
            ShouldFailValidation("ChangeSpeedNoTerm.xml", "<changeSpeed> requires a <term> child");
        }

        [Test()]
        public void ChangeDirectionRequiresDirection()
        {
            ShouldFailValidation("ChangeDirectionNoDirection.xml", "<changeDirection> requires a <direction> child");
        }

        [Test()]
        public void AccelRequiresTerm()
        {
            ShouldFailValidation("AccelNoTerm.xml", "<accel> requires a <term> child");
        }

        [Test()]
        public void RepeatRequiresTimes()
        {
            ShouldFailValidation("RepeatNoTimes.xml", "<repeat> requires a <times> child");
        }

        [Test()]
        public void RepeatRequiresAction()
        {
            ShouldFailValidation("RepeatNoAction.xml", "<repeat> requires an <action> or <actionRef> child");
        }
    }
}
