namespace TheTankGame.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using TheTankGame.Entities.Miscellaneous;
    using TheTankGame.Entities.Miscellaneous.Contracts;
    using TheTankGame.Entities.Parts;
    using TheTankGame.Entities.Parts.Contracts;
    using TheTankGame.Entities.Vehicles;
    using TheTankGame.Entities.Vehicles.Contracts;

    [TestFixture]
    public class BaseVehicleTests
    {
        private Type vehicleType;

        [SetUp]
        public void SetUp()
        {
            this.vehicleType = typeof(BaseVehicle);
        }

        [Test]
        public void TestFields()
        {
            var expctedFields = new[]
            {
                "weight",
                "price",
                "attack",
                "defense",
                "hitPoints",
                "model",
                "assembler",
                "orderedParts"
            };

            foreach (var field in expctedFields)
            {
                var currentField = this.vehicleType.GetField(field, (BindingFlags)62);

                Assert.That(currentField, Is.Not.Null);
            }
        }

        [Test]
        public void TestConstructor()
        {


            var constructor = this.vehicleType.GetConstructors((BindingFlags)62).FirstOrDefault();
            var expectedParams = new[]
            {
                "model",
                "weight",
                "price",
                "attack",
                "defense",
                "hitPoints",
                "assembler",
            };

            var actualParams = constructor.GetParameters().Select(p => p.Name);

            var type = constructor.IsFamily;

            Assert.That(expectedParams, Is.EquivalentTo(actualParams));
            Assert.IsTrue(type);
        }

        [Test]
        public void TestProps()
        {
            var expectedProps = new[]
            {
                "Model",
                "Weight",
                "Price",
                "Attack",
                "Defense",
                "HitPoints",
                "TotalWeight",
                "TotalPrice",
                "TotalAttack",
                "TotalDefense",
                "TotalHitPoints",
                "Parts"
            };

            var actualProps = this.vehicleType.GetProperties().Select(p => p.Name);

            Assert.That(expectedProps, Is.EquivalentTo(actualProps));
        }

        [Test]
        public void TestMethods()
        {
            var expctedMehods = new[]
            {
                "AddArsenalPart",
                "AddShellPart",
                "AddEndurancePart",
                "ToString"
            };

            foreach (var method in expctedMehods)
            {
                var currentMethod = this.vehicleType.GetMethod(method, (BindingFlags)62);

                Assert.That(currentMethod, Is.Not.Null);
            }
        }

        [Test]
        public void TestAddArsenalPart()
        {
            IPart part = new ArsenalPart("Cannon-KA2", 300, 500, 450);

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            vehicle.AddArsenalPart(part);

            IList<string> orderedParts = (IList<string>)this.vehicleType.GetField("orderedParts", (BindingFlags)62).GetValue(vehicle);
            IAssembler assembler = (IAssembler)this.vehicleType.GetField("assembler", (BindingFlags)62).GetValue(vehicle);

            Assert.AreEqual(orderedParts.Count, 1);
            Assert.AreEqual(assembler.ArsenalParts.Count, 1);
        }

        [Test]
        public void TestAddShellPart()
        {
            IPart part = new ShellPart("Cannon-KA2", 300, 500, 450);

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            vehicle.AddShellPart(part);

            IList<string> orderedParts = (IList<string>)this.vehicleType.GetField("orderedParts", (BindingFlags)62).GetValue(vehicle);
            IAssembler assembler = (IAssembler)this.vehicleType.GetField("assembler", (BindingFlags)62).GetValue(vehicle);

            Assert.AreEqual(orderedParts.Count, 1);
            Assert.AreEqual(assembler.ShellParts.Count, 1);
        }

        [Test]
        public void TestAddEndurancePart()
        {
            IPart part = new EndurancePart("Cannon-KA2", 300, 500, 450);

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            vehicle.AddEndurancePart(part);

            IList<string> orderedParts = (IList<string>)this.vehicleType.GetField("orderedParts", (BindingFlags)62).GetValue(vehicle);
            IAssembler assembler = (IAssembler)this.vehicleType.GetField("assembler", (BindingFlags)62).GetValue(vehicle);

            Assert.AreEqual(orderedParts.Count, 1);
            Assert.AreEqual(assembler.EnduranceParts.Count, 1);
        }

        [Test]
        public void TestParts()
        {
            List<IPart> expectedParts = new List<IPart>();
            IPart part;

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            part = new ArsenalPart("2424", 300, 500, 450);
            vehicle.AddArsenalPart(part);
            expectedParts.Add(part);


            part = new ShellPart("KA2", 300, 500, 450);
            vehicle.AddShellPart(part);
            expectedParts.Add(part);


            part = new ShellPart("KA", 300, 500, 450);
            vehicle.AddShellPart(part);
            expectedParts.Add(part);

            part = new EndurancePart("Cannon-KA2", 300, 500, 450);
            vehicle.AddEndurancePart(part);
            expectedParts.Add(part);

            Assert.That(vehicle.Parts, Is.EqualTo(expectedParts));
        }

        [Test]
        public void TestModel()
        {
            IVehicle vehicle;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger(" ", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler()));
        }

        [Test]
        public void TestWeight()
        {
            IVehicle vehicle;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("rewf", 0, 10, 1000, 1000, 1000, new VehicleAssembler()));
        }

        [Test]
        public void TestPrice()
        {
            IVehicle vehicle;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("rewf", 1000, 0, 10, 1000, 1000, new VehicleAssembler()));
        }

        [Test]
        public void TestAttack()
        {
            IVehicle vehicle;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("rewf", 1000, 10, -1, 10, 1000, new VehicleAssembler()));
        }

        [Test]
        public void TestDefense()
        {
            IVehicle vehicle;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("rewf", 1000, 10, 30, -1, 10, new VehicleAssembler()));
        }

        [Test]
        public void TestHitPoints()
        {
            IVehicle vehicle;

            Assert.Throws<ArgumentException>(() => vehicle = new Revenger("rewf", 1000, 10, 30, 10, -1, new VehicleAssembler()));
        }

        [Test]
        public void TestVehicle()
        {
            IVehicle vehicle = new Revenger("AKU", 1000, 43, 65, 54, 76, new VehicleAssembler());

            Assert.IsTrue(this.vehicleType.IsAbstract);
            Assert.AreEqual(vehicle.Model, "AKU");
            Assert.AreEqual(vehicle.Weight, 1000);
            Assert.AreEqual(vehicle.Price, 43);
            Assert.AreEqual(vehicle.Attack, 65);
            Assert.AreEqual(vehicle.Defense, 54);
            Assert.AreEqual(vehicle.HitPoints, 76);
        }

        [Test]
        public void TestTotalWeight()
        {
            IPart part;

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            part = new ArsenalPart("2424", 100, 500, 450);
            vehicle.AddArsenalPart(part);

            part = new ShellPart("KA2", 100, 500, 450);
            vehicle.AddShellPart(part);

            part = new ShellPart("KA", 100, 500, 450);
            vehicle.AddShellPart(part);

            part = new EndurancePart("Cannon-KA2", 100, 500, 450);
            vehicle.AddEndurancePart(part);

            Assert.AreEqual(vehicle.TotalWeight, 1400);
        }

        [Test]
        public void TestTotalPrice()
        {
            IPart part;

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            part = new ArsenalPart("2424", 100, 500, 450);
            vehicle.AddArsenalPart(part);

            part = new ShellPart("KA2", 100, 500, 450);
            vehicle.AddShellPart(part);

            part = new ShellPart("KA", 100, 500, 450);
            vehicle.AddShellPart(part);

            part = new EndurancePart("Cannon-KA2", 100, 500, 450);
            vehicle.AddEndurancePart(part);

            Assert.AreEqual(vehicle.TotalPrice, 3000);
        }

        [Test]
        public void TestTotalAttack()
        {
            IPart part;

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            part = new ArsenalPart("2424", 100, 500, 50);
            vehicle.AddArsenalPart(part);

            part = new ArsenalPart("KA2", 100, 500, 50);
            vehicle.AddArsenalPart(part);

            part = new ArsenalPart("KA", 100, 500, 50);
            vehicle.AddArsenalPart(part);

            Assert.AreEqual(vehicle.TotalAttack, 1150);
        }

        [Test]
        public void TestTotalDefense()
        {
            IPart part;

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            part = new ShellPart("2424", 100, 500, 50);
            vehicle.AddShellPart(part);

            part = new ShellPart("KA2", 100, 500, 50);
            vehicle.AddShellPart(part);

            part = new ShellPart("KA", 100, 500, 50);
            vehicle.AddShellPart(part);

            Assert.AreEqual(vehicle.TotalDefense, 1150);
        }

        [Test]
        public void TestTotalHitPoints()
        {
            IPart part;

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            part = new EndurancePart("2424", 100, 500, 50);
            vehicle.AddEndurancePart(part);

            part = new EndurancePart("KA2", 100, 500, 50);
            vehicle.AddEndurancePart(part);

            part = new EndurancePart("KA", 100, 500, 50);
            vehicle.AddEndurancePart(part);

            Assert.AreEqual(vehicle.TotalHitPoints, 1150);
        }

        [Test]
        public void TestToString()
        {
            IPart part;

            IVehicle vehicle = new Revenger("AKU", 1000, 1000, 1000, 1000, 1000, new VehicleAssembler());

            part = new ArsenalPart("2424", 300, 500, 450);
            vehicle.AddArsenalPart(part);

            part = new ShellPart("KA2", 300, 500, 450);
            vehicle.AddShellPart(part);

            part = new ShellPart("KA", 300, 500, 450);
            vehicle.AddShellPart(part);

            part = new EndurancePart("Cannon-KA2", 300, 500, 450);
            vehicle.AddEndurancePart(part);

            string actualResult = vehicle.ToString();
            string expectedResult= "Revenger - AKU\r\nTotal Weight: 2200.000\r\nTotal Price: 3000.000\r\nAttack: 1450\r\nDefense: 1900\r\nHitPoints: 1450\r\nParts: 2424, KA2, KA, Cannon-KA2";

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}