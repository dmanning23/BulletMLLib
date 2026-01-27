using NUnit.Framework;
using System;
using BulletMLLib;
using System.IO;
using FilenameBuddy;
using BulletMLSample;

namespace BulletMLTests
{
    [TestFixture()]
    public class Test
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
        public void ValidateTestData()
        {
            //Get all the xml files in the Content\\Samples directory
            var filename = new Filename("");
            foreach (var source in Directory.GetFiles(filename.GetPath(), "*.xml"))
            {
                //load & validate the pattern
                BulletPattern pattern = new BulletPattern(manager);
                pattern.ParseXML(source);
            }
        }
    }
}

