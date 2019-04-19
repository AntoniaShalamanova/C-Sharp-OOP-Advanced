using Moq;
using NUnit.Framework;

namespace Skeleton.Tests
{
    public class MoqHeroTests
    {
        private const string HeroName = "Pesho";

        [Test]
        public void HeroGainsXPWhenTheTargetDies()
        {
            Mock<ITarget> fakeTarget = new Mock<ITarget>();
            fakeTarget.Setup(p => p.Health).Returns(0);
            fakeTarget.Setup(p => p.GiveExperience()).Returns(10);
            fakeTarget.Setup(p => p.IsDead()).Returns(true);

            Mock<IWeapon> fakeWeapon = new Mock<IWeapon>();

            Hero hero = new Hero(HeroName, fakeWeapon.Object);

            hero.Attack(fakeTarget.Object);

            int actualResult = hero.Experience;

            int expectedResult = 10;

            Assert.AreEqual(expectedResult,
                actualResult,
                "Hero does not gains XP when target dies");
        }
    }
}
