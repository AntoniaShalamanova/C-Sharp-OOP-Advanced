using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using StorageMaster.Entities.Products;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    public class ProductTests
    {
        private Type productType;

        [SetUp]
        public void SetUp()
        {
            this.productType = GetType("Product");
        }

        [Test]
        public void ValidateAllProducts()
        {
            var expectedProducts = new[]
            {
                "Gpu",
                "HardDrive",
                "Product",
                "Ram",
                "SolidStateDrive"
            };

            foreach (var expectedProduct in expectedProducts)
            {
                var currentProduct = GetType(expectedProduct);

                Assert.That(currentProduct, Is.Not.Null);
            }
        }

        [Test]
        public void ValidateProductProperties()
        {
            var expectedProperties = new Dictionary<string, Type>
            {
                { "Price", typeof(double)},
                { "Weight", typeof(double)}
            };

            var actualProperties = this.productType
                .GetProperties();

            foreach (var actualProperty in actualProperties)
            {
                var isValidProperty = expectedProperties
                    .Any(p => p.Key == actualProperty.Name &&
                              p.Value == actualProperty.PropertyType);

                Assert.That(isValidProperty);
            }
        }

        [Test]
        public void ValidateProductFields()
        {
            var actualField = this.productType
                .GetField("price", (BindingFlags)62);

            Assert.That(actualField, Is.Not.Null);

            Assert.That(actualField.FieldType, 
                Is.EqualTo(typeof(double)));
        }

        [Test]
        public void ValidateProductIsAbstract()
        {
            Assert.That(this.productType.IsAbstract);
        }

        [Test]
        public void ValidateProductChildClasses()
        {
            var childClasses = new[]
            {
                typeof(Gpu),
                typeof(HardDrive),
                typeof(Ram),
                typeof(SolidStateDrive)
            };

            foreach (var childClass in childClasses)
            {
                Assert.That(childClass.BaseType, 
                    Is.EqualTo(this.productType));
            }
        }

        [Test]
        public void ValidateProductConstructor()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;

            var constructor = this.productType
                .GetConstructors(flags)
                .FirstOrDefault();

            Assert.That(constructor, Is.Not.Null);

            var actualParams = constructor.GetParameters()
                .Select(p=>p.ParameterType);

            var expectedParams = new[]
            {
                typeof(double),
                typeof(double)
            };

            Assert.That(expectedParams, Is.EqualTo(actualParams));
        }

        private Type GetType(string type)
        {
            var result = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            return result;
        }
    }
}
