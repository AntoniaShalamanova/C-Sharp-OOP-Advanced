using System;

namespace _7CustomList.Core
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
            string input = Console.ReadLine();

            while (input != "END")
            {
                this.commandInterpreter.ExecuteCommand(input);

                input = Console.ReadLine();
            }
        }
    }
}
