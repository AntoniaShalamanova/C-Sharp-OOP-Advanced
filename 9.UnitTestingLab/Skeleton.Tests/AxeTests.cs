using NUnit.Framework;

namespace Tests
{
    public class AxeTests
    {
        private const int AxeAttack = 10;
        private const int AxeDurability = 10;
        private const int DummyHealth = 10;
        private const int DummyXP = 10;
        private Axe axe;
        private Dummy target;

        [SetUp]
        public void TestInit()
        {
            this.axe = new Axe(AxeAttack, AxeDurability);
            this.target = new Dummy(DummyHealth, DummyXP);
        }

        [Test]
        public void AttackingWithBrokenWeaponThrowException()
        {
            this.axe = new Axe(AxeAttack, 0);

            Assert.That(() => axe.Attack(target), 
                Throws.InvalidOperationException.With.Message.EqualTo("Axe is broken."),
                "Attacking with broken weapon does not throw exception");
        }

        [Test]
        public void WeaponLosesDurabilityAfterAttack()
        {
            axe.Attack(target);

            int actualResult = axe.DurabilityPoints;

            int expectedResult = 9;

            Assert.AreEqual(expectedResult, 
                actualResult,
                "Weapon does not lose durability after attak");
        }
    }
}