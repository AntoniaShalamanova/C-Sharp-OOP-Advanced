using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Internal;
using StorageMaster.Entities.Products;
using StorageMaster.Entities.Storage;
using StorageMaster.Entities.Vehicles;

namespace StorageMaster.BusinessLogic.Tests
{
    [TestFixture]
    public class StorageMasterBusinessLogicTests
    {
        private Type storageMasterType;
        private object instance;
        private FieldInfo storageRegistryField;

        [SetUp]
        public void SetUp()
        {
            this.storageMasterType = GetType("StorageMaster");

            this.instance = Activator
                .CreateInstance(this.storageMasterType);

            this.storageRegistryField = this.storageMasterType
                .GetField("storageRegistry", (BindingFlags)62);
        }

        [Test]
        public void ValidateAddProductMethod()
        {
            string product = "Ram";
            double price = 230.5;

            MethodInfo addProductMethod = this.storageMasterType
                .GetMethod("AddProduct", BindingFlags.Public |
                                         BindingFlags.Instance);

            var actualResult = addProductMethod
                .Invoke(this.instance,
                new object[] { product, price });
            var expectedResult = "Added Ram to pool";

            Assert.AreEqual(expectedResult, actualResult);

            var productsPoolField = (Dictionary<string, Stack<Product>>)
                this.storageMasterType
                .GetField("productsPool", (BindingFlags)62)
                .GetValue(this.instance);

            Assert.AreEqual(1, productsPoolField[product].Count);
        }

        [Test]
        public void ValidateRegisterStorageMethod()
        {
            string storageType = "Warehouse";
            string storageName = "House";

            MethodInfo registerStorageMethod = this.storageMasterType
                .GetMethod("RegisterStorage", (BindingFlags)62);

            var actualResult = registerStorageMethod
                .Invoke(this.instance,
                new object[] { storageType, storageName });
            var expectedResult = "Registered House";

            Assert.AreEqual(expectedResult, actualResult);

            var storageRegistryValue = (Dictionary<string, Storage>)
                this.storageRegistryField
                    .GetValue(this.instance);

            Assert.AreEqual(storageName,
                storageRegistryValue[storageName].Name);
        }

        [Test]
        public void ValidateSelectVehicle()
        {
            string storageType = "Warehouse";
            string storageName = "House";

            MethodInfo registerStorageMethod = this.storageMasterType
                .GetMethod("RegisterStorage", (BindingFlags)62);

            registerStorageMethod.Invoke(this.instance,
                new object[] { storageType, storageName });

            MethodInfo selectVehicleMethod = this.storageMasterType
                .GetMethod("SelectVehicle", (BindingFlags)62);

            var actualResult = selectVehicleMethod.Invoke(this.instance, new object[] { storageName, 0 });
            var expectedResult = "Selected Semi";

            Assert.AreEqual(expectedResult, actualResult);

            FieldInfo currentVehicleField = this.storageMasterType
                .GetField("currentVehicle", (BindingFlags)62);

            var actualVehicle = currentVehicleField.GetValue(this.instance).GetType().Name;
            var expectedVehicle = typeof(Semi).Name;

            Assert.AreEqual(expectedVehicle, actualVehicle);
        }

        [Test]
        public void ValidateLoadVehicle()
        {
            FieldInfo currentVehicleField = this.storageMasterType
                .GetField("currentVehicle", (BindingFlags)62);

            currentVehicleField.SetValue(this.instance, new Van());

            MethodInfo addProductMethod = this.storageMasterType
                .GetMethod("AddProduct", (BindingFlags)62);

            addProductMethod.Invoke(this.instance, new object[] { "HardDrive", 20 });
            addProductMethod.Invoke(this.instance, new object[] { "HardDrive", 150 });
            addProductMethod.Invoke(this.instance, new object[] { "Gpu", 200 });

            MethodInfo loadVehicleMethod = this.storageMasterType
                .GetMethod("LoadVehicle", (BindingFlags)62);

            var actualResult = loadVehicleMethod
                .Invoke(this.instance, new object[] { new string[] { "HardDrive", "HardDrive", "Gpu" } });
            var expectedResult = "Loaded 2/3 products into Van";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ValidateSendVehicleTo()
        {
            MethodInfo registerStorageMethod = this.storageMasterType
                .GetMethod("RegisterStorage", (BindingFlags)62);

            registerStorageMethod.Invoke(this.instance,
                new object[] { "AutomatedWarehouse", "Pesho" });
            registerStorageMethod.Invoke(this.instance,
                new object[] { "Warehouse", "Gosho" });

            MethodInfo sendVehicleToMethod = this.storageMasterType
                .GetMethod("SendVehicleTo", (BindingFlags)62);

            var actualResult = sendVehicleToMethod.Invoke(this.instance,
                new object[] { "Gosho", 0, "Pesho" });

            var expectedResult = "Sent Semi to Pesho (slot 1)";

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ValidateUnloadVehicle()
        {
            string storageType = "DistributionCenter";
            string storageName = "Center";

            MethodInfo registerStorageMethod = this.storageMasterType
                .GetMethod("RegisterStorage", (BindingFlags)62);

            registerStorageMethod.Invoke(this.instance,
                    new object[] { storageType, storageName });

            MethodInfo selectVehicleMethod = this.storageMasterType
                .GetMethod("SelectVehicle", (BindingFlags)62);

            selectVehicleMethod.Invoke(this.instance,
                new object[] { storageName, 0 });

            MethodInfo addProductMethod = this.storageMasterType
                .GetMethod("AddProduct", (BindingFlags)62);

            addProductMethod.Invoke(this.instance, new object[] { "HardDrive", 20 });
            addProductMethod.Invoke(this.instance, new object[] { "HardDrive", 150 });
            addProductMethod.Invoke(this.instance, new object[] { "Gpu", 200 });

            MethodInfo loadVehicleMethod = this.storageMasterType
                .GetMethod("LoadVehicle", (BindingFlags)62);

            loadVehicleMethod.Invoke(this.instance,
                new object[] { new string[] { "HardDrive", "HardDrive", "Gpu" } });

            MethodInfo unloadVehicleMethod = this.storageMasterType
                .GetMethod("UnloadVehicle", (BindingFlags)62);

            var actualResult = unloadVehicleMethod.Invoke(this.instance,
                new object[] { storageName, 0 });

            var expectedResult = "Unloaded 2/2 products at Center";

            Assert.AreEqual(expectedResult, actualResult);
        }

        private Type GetType(string type)
        {
            var targetType = typeof(StartUp)
                .Assembly
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            return targetType;
        }
    }
}
