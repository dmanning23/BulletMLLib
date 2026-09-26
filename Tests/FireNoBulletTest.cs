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
    public class FireNoBulletTest
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
        public void FireWithoutBulletThrowsDescriptiveError()
        {
            var filename = new Filename(@"Invalid/FireNoBullet.xml");
            var ex = Should.Throw<Exception>(() => pattern.ParseXML(filename.File));
            ex.InnerException.ShouldBeOfType<InvalidDataException>();
            ex.InnerException.Message.ShouldContain("\"broken\" has no bullet or bulletRef");
        }
    }
}
