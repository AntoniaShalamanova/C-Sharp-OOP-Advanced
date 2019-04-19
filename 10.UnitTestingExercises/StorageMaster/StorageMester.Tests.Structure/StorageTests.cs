using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;

namespace StorageMaster.Tests.Structure
{
    [TestFixture()]
    public class StorageTests
    {
        private Type storageType;

        [SetUp]
        public void SetUp()
        {
            this.storageType = GetType("Storage");
        }

        [Test]
        public void ValidateAllStorages()
        {
            var expectedStorages = new[]
            {
                "AutomatedWarehouse",
                "DistributionCenter",
                "Storage",
                "Warehouse"
            };

            foreach (var expectedStorage in expectedStorages)
            {
                var currentStorage = GetType(expectedStorage);

                Assert.That(currentStorage, Is.Not.Null);
            }
        }

        [Test]
        public void ValidateStorageProperties()
        {
            var expectedProperties = new Dictionary<string, Type>
            {
                { "Name",typeof(string)},
                { "Capacity",typeof(int)},
                { "GarageSlots",typeof(int)},
                { "IsFull",typeof(bool)},
                { "Garage",typeof(IReadOnlyCollection<Vehicle>)},
                { "Products",typeof(IReadOnlyCollection<Product>)},
            };

            var actualProperties = this.storageType.GetProperties();

            foreach (var actualProperty in actualProperties)
            {
                var IsValidProperty = expectedProperties
                    .Any(p => p.Key == actualProperty.Name &&
                              p.Value == actualProperty.PropertyType);

                Assert.That(IsValidProperty);
            }
        }

        [Test]
        public void ValidateStorageMethods()
        {
            var expectedMethods = new[]
            {
                new Method(typeof(Vehicle), "GetVehicle", typeof(int)),
                new Method(typeof(int), "SendVehicleTo", typeof(int), typeof(Storage)),
                new Method(typeof(int), "UnloadVehicle", typeof(int)),
                new Method(typeof(void), "InitializeGarage", typeof(IEnumerable<Vehicle>)),
                new Method(typeof(int), "AddVehicle", typeof(Vehicle)),
            };

            foreach (var expectedMethod in expectedMethods)
            {
                var currentMethod = this.storageType
                    .GetMethod(expectedMethod.Name, (BindingFlags)62);

                Assert.That(currentMethod, Is.Not.Null);

                Assert.AreEqual(expectedMethod.ReturnType, currentMethod.ReturnType);

                var expectedParameters = expectedMethod.Parameters;
                var currentParameters = currentMethod.GetParameters()
                    .Select(p => p.ParameterType)
                    .ToArray();

                if (expectedParameters.Length != currentParameters.Length)
                {
                    Assert.That(false);
                }

                for (int i = 0; i < expectedParameters.Length; i++)
                {
                    Assert.That(expectedParameters[i],
                        Is.EqualTo(currentParameters[i]));
                }
            }
        }

        [Test]
        public void ValidateStorageFields()
        {
            var expectedFields = new[]
            {
                "garage",
                "products"
            };

            foreach (var expectedField in expectedFields)
            {
                var currentField= this.storageType
                    .GetField(expectedField,
                        BindingFlags.NonPublic | BindingFlags.Instance);

                Assert.That(currentField, Is.Not.Null);
            }
        }

        [Test]
        public void ValidateStorageIsAbstract()
        {
            Assert.That(this.storageType.IsAbstract);
        }

        [Test]
        public void ValidateStorageChildClasses()
        {
            var childClasses = new[]
            {
                typeof(AutomatedWarehouse),
                typeof(DistributionCenter),
                typeof(Warehouse)
            };

            foreach (var childClass in childClasses)
            {
                Assert.That(childClass.BaseType, Is.EqualTo(this.storageType));
            }
        }

        [Test]
        public void ValidateStorageConstructor()
        {
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;

            var constructor = this.storageType
                .GetConstructors(flags)
                .FirstOrDefault();

            Assert.That(constructor, Is.Not.Null);

            var expectedParameters = new[]
            {
                typeof(string),
                typeof(int),
                typeof(int),
                typeof(IEnumerable<Vehicle>),
            };

            var actualParameters = constructor.GetParameters()
                .Select(p=>p.ParameterType)
                .ToArray();

            Assert.That(expectedParameters, Is.EqualTo(actualParameters));
        }

        private Type GetType(string type)
        {
            var result = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            return result;
        }

        private class Method
        {
            public Method(Type returnType, string name,
                params Type[] parameters)
            {
                this.ReturnType = returnType;
                this.Name = name;
                this.Parameters = parameters;
            }

            public Type ReturnType { get; set; }

            public string Name { get; set; }

            public Type[] Parameters { get; set; }
        }
    }
}
