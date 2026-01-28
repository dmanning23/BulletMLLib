using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using Shouldly;
using System;

namespace BulletMLTests
{
    [TestFixture()]
    public class TestAimXml
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
        public void CorrectNumberOfBullets()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.movers.Count.ShouldBe(1);
        }

        [Test()]
        public void CorrectNumberOfBullets1()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            manager.Update();
            manager.movers.Count.ShouldBe(2);
        }

        [Test()]
        public void CorrectNumberOfBullets2()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //run the thing ten times
            for (int i = 2; i < 12; i++)
            {
                manager.Update();
                manager.movers.Count.ShouldBe(i);
            }

            //there should be 11 bullets
            manager.movers.Count.ShouldBe(11);
        }

        [Test()]
        public void CorrectDirection()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //run the thing ten times
            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            for (int i = 1; i < manager.movers.Count; i++)
            {
                Mover testDude = manager.movers[i];
                float direction = testDude.Direction * 180 / (float)Math.PI;
                ((int)direction).ShouldBe(90);
            }
        }

        [Test()]
        public void SpeedInitializedCorrect()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //get the fire task
            FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
            testTask.ShouldNotBeNull();

            testTask.InitialSpeedTask.ShouldNotBeNull();
            testTask.SequenceSpeedTask.ShouldNotBeNull();
        }

        [Test()]
        public void SpeedInitializedCorrect1()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
            testTask.InitialSpeedTask.ShouldNotBeNull();
            testTask.SequenceSpeedTask.ShouldNotBeNull();
        }

        [Test()]
        public void SpeedInitializedCorrect2()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
            testTask.InitialSpeedTask.GetNodeValue(mover).ShouldBe(1.0f);
            testTask.SequenceSpeedTask.GetNodeValue(mover).ShouldBe(1.0f);
        }

        [Test()]
        public void SpeedInitializedCorrect3()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
            testTask.NumTimesInitialized.ShouldBe(1);
        }

        [Test()]
        public void SpeedInitializedCorrect4()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            FireTask testTask = mover.FindTaskByLabel("testFire") as FireTask;
            testTask.FireSpeed.ShouldBe(1.0f);
        }

        [Test()]
        public void CorrectSpeed()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //run the thing ten times
            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            //check the top bullet
            Mover testDude = manager.movers[0];
            testDude.Speed.ShouldBe(0);

            //check the second bullet
            testDude = manager.movers[1];
            testDude.Speed.ShouldBe(1);
        }

        [Test()]
        public void CorrectSpeed1()
        {
            dude.pos.X = 100.0f;
            dude.pos.Y = 0.0f;
            var filename = new Filename(@"Aim.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);

            //run the thing ten times
            for (int i = 0; i < 10; i++)
            {
                manager.Update();
            }

            //check the first bullet

            //check the second bullet

            for (int i = 1; i < manager.movers.Count; i++)
            {
                Mover testDude = manager.movers[i];
                testDude.Speed.ShouldBe(i);
            }
        }
    }
}

