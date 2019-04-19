using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using StorageMaster;
using StorageMaster.Entities.Products;

namespace StorageMaster.Tests.Structure
{
    [TestFixture]
    public class VehicleTests
    {
        private Type vehicle;

        [SetUp]
        public void SetUp()
        {
            this.vehicle = this.GetType("Vehicle");
        }

        [Test]
        public void ValidateAllVehicles()
        {
            var expectedTypes = new[]
            {
                "Semi",
                "Truck",
                "Van",
                "Vehicle"
            };

            foreach (var type in expectedTypes)
            {
                var currentType = GetType(type);

                Assert.That(currentType, Is.Not.Null,
                    $"{type} does not exists!");
            }
        }

        [Test]
        public void ValidateVehicleProperties()
        {
            var vehicleType = this.vehicle;

            var actualproperties = vehicleType
                .GetProperties();

            var expectedProperties = new Dictionary<string, Type>
            {
                {"Capacity", typeof(int)},
                { "Trunk", typeof(IReadOnlyCollection<Product>)},
                { "IsFull", typeof(bool)},
                { "IsEmpty", typeof(bool)}
            };

            foreach (var actualProperty in actualproperties)
            {
                var isValidProperty = expectedProperties
                    .Any(p => p.Key == actualProperty.Name &&
                              p.Value == actualProperty.PropertyType);

                Assert.That(isValidProperty,
                    $"{actualProperty.Name} does not exists!");
            }
        }

        [Test]
        public void ValidateVehicleMethods()
        {
            var expectedMethods = new Method[]
            {
                new Method(typeof(void), "LoadProduct", typeof(Product)),
                new Method(typeof(Product), "Unload")
            };

            foreach (var expectedMethod in expectedMethods)
            {
                var currentMethod = this.vehicle
                    .GetMethod(expectedMethod.Name);

                Assert.That(currentMethod, Is.Not.Null,
                    $"{expectedMethod.Name} method does not exists!");

                var isValidReturnType = currentMethod
                    .ReturnType == expectedMethod.ReturnType;

                Assert.That(isValidReturnType,
                    $"{expectedMethod.Name} invalid return type!");

                var expectedMethodParams = expectedMethod.InputParameters;
                var currentMethodParams = currentMethod.GetParameters();

                if (expectedMethodParams.Length != currentMethodParams.Length)
                {
                    Assert.That(false,
                        $"{expectedMethod.Name} invalid parameters!");
                }

                for (int i = 0; i < expectedMethodParams.Length; i++)
                {
                    var currentParam = currentMethodParams[i].ParameterType;
                    var expectedParam = expectedMethodParams[i];

                    Assert.AreEqual(expectedParam, currentParam,
                        $"{expectedMethod.Name} invalid parameter!");
                }
            }
        }

        [Test]
        public void ValidateVehicleFields()
        {
            var trunkField = this.vehicle
                .GetField("trunk", BindingFlags.NonPublic |
                                   BindingFlags.Instance);

            Assert.That(trunkField, Is.Not.Null,
                $"Invalid field!");
        }

        [Test]
        public void ValidateVehicleIsAbstract()
        {
            Assert.That(this.vehicle.IsAbstract,
                $"Vehicle class must be abstract!");
        }

        [Test]
        public void ValidateVehicleChildClasses()
        {
            var childClasses = new[]
            {
                GetType("Semi"),
                GetType("Van"),
                GetType("Truck")
            };

            foreach (var childClass in childClasses)
            {
                Assert.That(childClass.BaseType, Is.EqualTo(this.vehicle),
                    $"{childClass.Name} does not inherit {this.vehicle.Name}!");
            }
        }

        [Test]
        public void ValidateVehicleConstructor()
        {
            var flags = BindingFlags.NonPublic |
                        BindingFlags.Instance;

            var constructor = this.vehicle
                .GetConstructors(flags)
                .FirstOrDefault();

            Assert.That(constructor, Is.Not.Null,
                $"Constructor does not exists!");

            var constructorParams = constructor.GetParameters();

            Assert.That(constructorParams[0].ParameterType, 
                Is.EqualTo(typeof(int)),
                "Constructor has incorrect parameters!");
        }

        private Type GetType(string type)
        {
            var targetType = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            return targetType;
        }

        private class Method
        {
            public Method(Type returnType, string name,
                params Type[] inputParameters)
            {
                this.ReturnType = returnType;
                this.Name = name;
                this.InputParameters = inputParameters;
            }

            public Type ReturnType { get; set; }

            public string Name { get; set; }

            public Type[] InputParameters { get; set; }
        }
    }
}
