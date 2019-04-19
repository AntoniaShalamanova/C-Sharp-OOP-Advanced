namespace TheTankGame.Core
{
    using System;

    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(
            IReader reader,
            IWriter writer,
            ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;

            this.isRunning = false;
        }

        public void Run()
        {
            while (true)
            {
                string[] inputArgs = this.reader.ReadLine().Split();

                string result = this.commandInterpreter.ProcessInput(inputArgs);

                this.writer.WriteLine(result);

                if (inputArgs[0] == "Terminate")
                {
                    Environment.Exit(0);
                }
            }
        }
    }
}