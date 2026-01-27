using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class TestRepeatSequenceXml
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
        public void CorrectSpeed()
        {
            var filename = new Filename(@"RepeatSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            ClassicAssert.AreEqual(0, mover.Speed);
        }

        [Test()]
        public void CorrectSpeed1()
        {
            var filename = new Filename(@"RepeatSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            ClassicAssert.AreEqual(10, mover.Speed);
        }

        [Test()]
        public void CorrectSpeed2()
        {
            var filename = new Filename(@"RepeatSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            ClassicAssert.AreEqual(1, mover.Speed);
        }
    }
}

