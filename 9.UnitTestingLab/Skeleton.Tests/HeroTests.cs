using NUnit.Framework;

namespace Skeleton.Tests
{
    public class HeroTests
    {
        private const string HeroName = "Pesho";

        [Test]
        public void HeroGainsXPWhenTheTargetDies()
        {
            ITarget fakeTarget = new FakeTarget();
            IWeapon fakeWeapon = new FakeWeapon();
            Hero hero = new Hero(HeroName, fakeWeapon);

            hero.Attack(fakeTarget);

            int actualResult = hero.Experience;

            int expectedResult = 10;

            Assert.AreEqual(expectedResult,
                actualResult,
                "Hero does not gains XP when target dies");
        }
    }
}
