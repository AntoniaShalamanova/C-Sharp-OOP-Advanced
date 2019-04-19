using _03BarracksFactory.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace P03_BarraksWars.Core
{
    public class CommandInterpreter : ICommandInterpreter
    {
        private IRepository repository;
        private IUnitFactory unitFactory;

        public CommandInterpreter(IRepository repository,
            IUnitFactory unitFactory)
        {
            this.repository = repository;
            this.unitFactory = unitFactory;
        }

        public IExecutable InterpretCommand(string[] data, 
            string commandName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = assembly
                .GetTypes()
                .First(t => t.Name.ToLower() == commandName);

            var instance = Activator
                .CreateInstance(type,
                new object[] { data, this.repository, this.unitFactory });

            return (IExecutable)instance;
        }
    }
}
