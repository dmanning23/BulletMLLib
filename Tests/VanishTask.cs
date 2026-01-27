using BulletMLLib;
using BulletMLSample;
using FilenameBuddy;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace BulletMLTests
{
    [TestFixture()]
    public class VanishTask
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
        public void VanishTaskTest()
        {
            var filename = new Filename(@"Vanish.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            ClassicAssert.AreEqual(0, manager.movers.Count);
        }

        [Test()]
        public void NestedVanish()
        {
            var filename = new Filename(@"NestedVanish.xml");
            pattern.ParseXML(filename.File);
            Mover mover = (Mover)manager.CreateBullet();
            mover.InitTopNode(pattern.RootNode);
            manager.Update();
            ClassicAssert.AreEqual(0, manager.movers.Count);
        }
    }
}

