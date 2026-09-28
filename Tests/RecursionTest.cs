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
        public void ActionCanReferenceItselfAfterWaiting()
        {
            var filename = new Filename(@"RecursiveActionRef.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            //the action fires once a frame, and each call passes a speed one higher than the last
            var shots = manager.movers.Where(x => x.Label == "shot").ToList();
            shots.Count.ShouldBe(9);
            shots.Select(x => x.Speed).ShouldBe(Enumerable.Range(1, 9).Select(x => (float)x));
        }

        [Test()]
        public void LongRunningRecursionDoesNotOverflowTheStack()
        {
            var filename = new Filename(@"RecursiveActionRef.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //one hour at 60fps
            for (int i = 0; i < 216000; i++)
            {
                mover.Update();
            }

            mover.TasksFinished().ShouldBeFalse();
        }

        [Test()]
        public void LongRunningRecursionKeepsCorrectParams()
        {
            var filename = new Filename(@"RecursiveActionRef.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 1000; i++)
            {
                manager.Update();
            }

            //each call passes a speed one higher than the last, for every call
            var shots = manager.movers.Where(x => x.Label == "shot").ToList();
            shots.Count.ShouldBe(999);
            shots.Select(x => x.Speed).ShouldBe(Enumerable.Range(1, 999).Select(x => (float)x));
        }

        [Test()]
        public void TopActionCanReferenceItself()
        {
            var filename = new Filename(@"RecursiveTopAction.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 216000; i++)
            {
                mover.Update();
            }

            mover.TasksFinished().ShouldBeFalse();
        }

        [Test()]
        public void RecursionWithZeroWaitRunsOncePerFrame()
        {
            var filename = new Filename(@"RecursiveZeroWait.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            //a wait of 0 would restart forever in one frame, so it's held to one call per frame
            manager.movers.Count(x => x.Label == "shot").ShouldBe(10);
        }

        [Test()]
        public void RecursiveFanPatternRuns()
        {
            var filename = new Filename(@"RecursiveFan.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 200; i++)
            {
                manager.Update();
            }

            manager.movers.Count.ShouldBeGreaterThan(1);
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
