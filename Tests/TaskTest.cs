using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using System;
using BulletMLLib;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class TaskTest
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
        public void OneAction()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks.Count.ShouldBe(1);
        }

        [Test()]
        public void OneAction1()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].ShouldBeOfType<ActionTask>();
        }

        [Test()]
        public void NoChildTasks()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].ChildTasks.Count.ShouldBe(0);
        }

        [Test()]
        public void NoParams()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].ParamList.Count.ShouldBe(0);
        }

        [Test()]
        public void NoOwner()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].Owner.ShouldBeNull();
        }

        [Test()]
        public void CorrectNode()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].Node.ShouldNotBeNull();
        }

        [Test()]
        public void NotFinished()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].TaskFinished.ShouldBeFalse();
        }

        [Test()]
        public void OkFinished()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            mover.Tasks[0].Run(mover).ShouldBe(RunStatus.End);
        }

        [Test()]
        public void TaskFinishedFlag()
        {
            var filename = new Filename(@"ActionOneTop.xml");
            BulletPattern pattern = new BulletPattern(manager);
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            mover.Tasks[0].Run(mover);

            mover.Tasks[0].TaskFinished.ShouldBeTrue();
        }
    }
}

