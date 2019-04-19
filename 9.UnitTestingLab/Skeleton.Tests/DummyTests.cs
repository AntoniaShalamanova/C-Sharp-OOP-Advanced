using NUnit.Framework;

namespace Skeleton.Tests
{
    public class DummyTests
    {
        private const int DummyHealth = 10;
        private const int DummyXP = 10;
        private Dummy target;

        [SetUp]
        public void TestInit()
        {
            this.target = new Dummy(DummyHealth, DummyXP);
        }

        [Test]
        public void DummyLosesHealthIfAttacked()
        {
            target.TakeAttack(2);

            int actualResult = target.Health;

            int expectedResult = 8;

            Assert.AreEqual(expectedResult, 
                actualResult,
                "Dummy does not lose health if attacked");
        }

        [Test]
        public void DeadDummyThrowsExceptionIfAttacked()
        {
            this.target = new Dummy(0, DummyXP);

            Assert.That(() => target.TakeAttack(1), 
                Throws.InvalidOperationException,
                "Dead dummy does not throw exception if attacked");
        }

        [Test]
        public void DeadDummyCanGiveXP()
        {
            this.target = new Dummy(0, DummyXP);

            int actualResult = target.GiveExperience();

            int expectedResult = 10;

            Assert.AreEqual(expectedResult, 
                actualResult,
                "Dead dummy can not give XP");
        }

        [Test]
        public void AliveDummyCantGiveXP()
        {
            this.target = new Dummy(1, DummyXP);

            Assert.That(() => target.GiveExperience(), 
                Throws.InvalidOperationException,
                "Alive dummy can give XP");
        }
    }
}
