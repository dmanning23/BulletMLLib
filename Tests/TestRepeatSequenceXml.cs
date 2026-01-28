using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;

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
            mover.Speed.ShouldBe(0);
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

            mover.Speed.ShouldBe(10);
        }

        [Test()]
        public void CorrectSpeed2()
        {
            var filename = new Filename(@"RepeatSequence.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();

            mover.Speed.ShouldBe(1);
        }
    }
}

