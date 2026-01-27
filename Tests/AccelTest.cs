using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Shouldly;
using NUnit.Framework.Legacy;

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
            ClassicAssert.AreEqual(20.0f, mover.Acceleration.X);
            ClassicAssert.AreEqual(40.0f, mover.Acceleration.Y);
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

            ClassicAssert.AreEqual(19.0f, mover.Acceleration.X);
            ClassicAssert.AreEqual(38.0f, mover.Acceleration.Y);
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

            ClassicAssert.AreEqual(10.0f, mover.Acceleration.X);
            ClassicAssert.AreEqual(20.0f, mover.Acceleration.Y);
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

            ClassicAssert.AreEqual(21.0f, mover.Acceleration.X);
            ClassicAssert.AreEqual(42.0f, mover.Acceleration.Y);
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

            ClassicAssert.AreEqual(30.0f, mover.Acceleration.X);
            ClassicAssert.AreEqual(60.0f, mover.Acceleration.Y);
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
            ClassicAssert.AreEqual(1.0f, myTask.Acceleration.X);
            ClassicAssert.AreEqual(2.0f, myTask.Acceleration.Y);
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
            ClassicAssert.AreEqual(10.0f, myNode.GetValue(myTask, mover));
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
            ClassicAssert.AreEqual(NodeType.relative, myNode.NodeType);
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
            ClassicAssert.AreEqual(NodeType.relative, myNode.NodeType);
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
            ClassicAssert.AreEqual(20.0f, myNode.GetValue(myTask, mover));
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
            ClassicAssert.AreEqual(10.0f, myTask.Duration);
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
            ClassicAssert.AreEqual(1.0f, myTask.Acceleration.X);
            ClassicAssert.AreEqual(2.0f, myTask.Acceleration.Y);
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

            ClassicAssert.AreEqual(21.0f, mover.Acceleration.X);
            ClassicAssert.AreEqual(42.0f, mover.Acceleration.Y);
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

            ClassicAssert.AreEqual(30.0f, mover.Acceleration.X);
            ClassicAssert.AreEqual(60.0f, mover.Acceleration.Y);
        }
    }
}

