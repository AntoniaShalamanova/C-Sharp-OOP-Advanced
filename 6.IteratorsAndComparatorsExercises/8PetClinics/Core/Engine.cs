using System;
using System.Linq;

namespace _8PetClinics.Core
{
    public class Engine
    {
        private CommandInterpreter commandInterpreter;

        public Engine(CommandInterpreter commandInterpreter)
        {
            this.commandInterpreter = commandInterpreter;
        }

        public void Run()
        {
            int commandsCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < commandsCount; i++)
            {
                try
                {
                    string[] input = Console.ReadLine()
                                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string commandType = input[0];
                    string[] commandArgs = input.Skip(1).ToArray();

                    this.commandInterpreter.ExecuteCommand(commandType, commandArgs);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
