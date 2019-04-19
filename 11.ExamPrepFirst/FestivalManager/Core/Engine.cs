﻿
using System;
using System.Linq;
namespace FestivalManager.Core
{
    using System.Reflection;
    using Contracts;
    using Controllers;
    using Controllers.Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        private IFestivalController festivalCоntroller;
        private ISetController setCоntroller;

        public Engine(IReader reader, IWriter writer,
            IFestivalController festivalController,
            ISetController setController)
        {
            this.reader = reader;
            this.writer = writer;
            this.festivalCоntroller = festivalController;
            this.setCоntroller = setController;
        }

        public void Run()
        {
            while (true)
            {
                var input = this.reader.ReadLine();

                if (input == "END")
                {
                    break;
                }

                string commandResult;

                try
                {
                    commandResult = this.ProcessCommand(input);
                }
                catch (TargetInvocationException ex)
                {
                    commandResult = "ERROR: " + ex.InnerException.Message;
                }
                catch (Exception ex)
                {
                    commandResult = "ERROR: " + ex.Message;
                }

                this.writer.WriteLine(commandResult);
            }

            var end = this.festivalCоntroller.ProduceReport();

            this.writer.WriteLine("Results:");
            this.writer.WriteLine(end);
        }

        public string ProcessCommand(string input)
        {
            string[] tokens = input.Split();

            string command = tokens[0];
            string[] args = tokens.Skip(1).ToArray();

            string result;
            if (command == "LetsRock")
            {
                result = this.setCоntroller.PerformSets();
            }
            else
            {
                var festivalControllerMethod = this.festivalCоntroller
                    .GetType()
                    .GetMethods()
                    .FirstOrDefault(x => x.Name == command);

                result = (string)festivalControllerMethod
                    .Invoke(this.festivalCоntroller, new object[] { args });

            }

            return result;
        }
    }
}