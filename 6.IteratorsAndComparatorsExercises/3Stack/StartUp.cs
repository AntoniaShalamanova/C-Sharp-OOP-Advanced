using System;
using System.Linq;

namespace _3Stack
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            CustomStack<int> customStack = new CustomStack<int>();

            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] inputArgs = input
                    .Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                string command = inputArgs[0];

                switch (command)
                {
                    case "Push":
                        inputArgs = inputArgs.Skip(1).ToArray();

                        foreach (var item in inputArgs)
                        {
                            customStack.Push(int.Parse(item));
                        }

                        break;

                    case "Pop":
                        try
                        {
                            customStack.Pop();
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    default:
                        break;
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(string.Join(Environment.NewLine, customStack));

            Console.WriteLine(string.Join(Environment.NewLine, customStack));
        }
    }
}
