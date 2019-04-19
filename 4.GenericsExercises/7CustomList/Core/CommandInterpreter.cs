using System;
using System.Linq;

namespace _7CustomList.Core
{
    public class CommandInterpreter
    {
        private CustomList<string> customList;

        public CommandInterpreter()
        {
            this.customList = new CustomList<string>();
        }

        public void ExecuteCommand(string input)
        {
            string[] inputArgs = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            string command = inputArgs[0];

            inputArgs = inputArgs.Skip(1).ToArray();

            string element = string.Empty;
            int index;

            switch (command)
            {
                case "Add":
                    element = inputArgs[0];
                    this.customList.Add(element);
                    break;

                case "Remove":
                    index = int.Parse(inputArgs[0]);
                    this.customList.Remove(index);
                    break;

                case "Contains":
                    element = inputArgs[0];
                    Console.WriteLine(this.customList.Contains(element));
                    break;

                case "Swap":
                    int index1 = int.Parse(inputArgs[0]);
                    int index2 = int.Parse(inputArgs[1]);

                    this.customList.Swap(index1, index2);
                    break;

                case "Greater":
                    element = inputArgs[0];
                    Console.WriteLine(this.customList.CountGreaterThan(element));
                    break;

                case "Max":
                    Console.WriteLine(this.customList.Max());
                    break;

                case "Min":
                    Console.WriteLine(this.customList.Min());
                    break;

                case "Print":
                    for (int i = 0; i < this.customList.Count; i++)
                    {
                        Console.WriteLine(this.customList[i]);
                    }
                    break;

                case "Sort":
                    this.customList.Sort();
                    break;

                default:
                    break;
            }
        }
    }
}
