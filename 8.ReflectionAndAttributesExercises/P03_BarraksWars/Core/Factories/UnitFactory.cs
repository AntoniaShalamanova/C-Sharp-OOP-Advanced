namespace _03BarracksFactory.Core.Factories
{
    using Contracts;
    using System;
    using System.Linq;
    using System.Reflection;

    public class UnitFactory : IUnitFactory
    {
        public IUnit CreateUnit(string unitType)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = assembly.GetTypes()
                .First(t => t.Name == unitType);

            var instance = Activator.CreateInstance(type);

            return (IUnit)instance;
        }
    }
}
