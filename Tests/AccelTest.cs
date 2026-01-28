using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Shouldly;

namespace BulletMLTests
{
    [TestFixture()]
    public class AccelTest
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
        public void CorrectSpeedAbs()
        {
            var filename = new Filename(@"AccelAbs.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);
            mover.Acceleration.X.ShouldBe(20.0f);
            mover.Acceleration.Y.ShouldBe(40.0f);
        }

        [Test()]
        public void CorrectSpeedAbs1()
        {
            var filename = new Filename(@"AccelAbs.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            mover.Acceleration.X.ShouldBe(19.0f);
            mover.Acceleration.Y.ShouldBe(38.0f);
        }

        [Test()]
        public void CorrectSpeedAbs2()
        {
            var filename = new Filename(@"AccelAbs.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            mover.Acceleration.X.ShouldBe(10.0f);
            mover.Acceleration.Y.ShouldBe(20.0f);
        }

        [Test()]
        public void CorrectSpeedRel()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            mover.Acceleration.X.ShouldBe(21.0f);
            mover.Acceleration.Y.ShouldBe(42.0f);
        }

        [Test()]
        public void CorrectSpeedRel1()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            mover.Acceleration.X.ShouldBe(30.0f);
            mover.Acceleration.Y.ShouldBe(60.0f);
        }

        [Test()]
        public void CorrectSpeedRel2()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            BulletMLTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel);
            myTask.ShouldNotBeNull();
        }

        [Test()]
        public void CorrectSpeedRel3()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            myTask.ShouldNotBeNull();
        }

        [Test()]
        public void CorrectSpeedRel4()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            myTask.Acceleration.X.ShouldBe(1.0f);
            myTask.Acceleration.Y.ShouldBe(2.0f);
        }

        [Test()]
        public void CorrectSpeedRel5()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.horizontal);
            myNode.GetValue(myTask, mover).ShouldBe(10.0f);
        }

        [Test()]
        public void CorrectSpeedRel6()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.vertical);
            myNode.NodeType.ShouldBe(NodeType.relative);
        }

        [Test()]
        public void CorrectSpeedRel7()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.horizontal);
            myNode.NodeType.ShouldBe(NodeType.relative);
        }

        [Test()]
        public void CorrectSpeedRel8()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            BulletMLNode myNode = myTask.Node.GetChild(NodeName.vertical);
            myNode.GetValue(myTask, mover).ShouldBe(20.0f);
        }

        [Test()]
        public void CorrectSpeedRel9()
        {
            var filename = new Filename(@"AccelRel.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            myTask.Duration.ShouldBe(10.0f);
        }

        [Test()]
        public void CorrectSpeedSeq()
        {
            var filename = new Filename(@"AccelSeq.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            AccelTask myTask = mover.FindTaskByLabelAndName("test", NodeName.accel) as AccelTask;
            myTask.Acceleration.X.ShouldBe(1.0f);
            myTask.Acceleration.Y.ShouldBe(2.0f);
        }

        [Test()]
        public void CorrectSpeedSeq1()
        {
            var filename = new Filename(@"AccelSeq.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            manager.Update();

            mover.Acceleration.X.ShouldBe(21.0f);
            mover.Acceleration.Y.ShouldBe(42.0f);
        }

        [Test()]
        public void CorrectSpeedSeq2()
        {
            var filename = new Filename(@"AccelSeq.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.Acceleration = new Vector2(20.0f, 40.0f);
            mover.InitTopNode(pattern.RootNode);

            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            mover.Acceleration.X.ShouldBe(30.0f);
            mover.Acceleration.Y.ShouldBe(60.0f);
        }
    }
}

