using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using System.Linq;
using BulletMLLib;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class RecursionTest
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
        public void BulletCanFireItself()
        {
            var filename = new Filename(@"RecursiveBulletRef.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 5; i++)
            {
                manager.Update();
            }

            //each split bullet fires a new one and vanishes, so there is always exactly one alive
            manager.movers.Count(x => x.Label == "split").ShouldBe(1);
        }

        [Test()]
        public void BulletCanFireItselfThroughActionRef()
        {
            var filename = new Filename(@"RecursiveBulletActionRef.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 5; i++)
            {
                manager.Update();
            }

            manager.movers.Count(x => x.Label == "split").ShouldBe(1);
        }

        [Test()]
        public void ActionRefToItselfThrows()
        {
            var filename = new Filename(@"Invalid/ActionRefSelfCycle.xml");
            var ex = Should.Throw<Exception>(() => pattern.ParseXML(filename.File));
            ex.InnerException.Message.ShouldContain("circular");
        }

        [Test()]
        public void IndirectActionRefCycleThrows()
        {
            var filename = new Filename(@"Invalid/ActionRefIndirectCycle.xml");
            var ex = Should.Throw<Exception>(() => pattern.ParseXML(filename.File));
            ex.InnerException.Message.ShouldContain("circular");
        }
    }
}
