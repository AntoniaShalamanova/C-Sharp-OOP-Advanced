using System;
using System.Linq;

namespace _1ListyIterator.Core
{
    public class Engine
    {
        private ListyIterator<string> listyIterator;

        internal void Run()
        {
            string[] createInput = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Skip(1)
                    .ToArray();

            this.listyIterator = new ListyIterator<string>(createInput);

            string input = Console.ReadLine();

            while (input != "END")
            {
                switch (input)
                {
                    case "Move":
                        Console.WriteLine(this.listyIterator.Move());
                        break;

                    case "HasNext":
                        Console.WriteLine(this.listyIterator.HasNext());
                        break;

                    case "PrintAll":
                        Console.WriteLine(this.listyIterator.PrintAll());
                        break;

                    case "Print":
                        try
                        {
                            Console.WriteLine(this.listyIterator.Print());
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }

                input = Console.ReadLine();
            }
        }
    }
}
