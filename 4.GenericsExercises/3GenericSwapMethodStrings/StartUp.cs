using System;
using System.Collections.Generic;
using System.Linq;

namespace _3GenericSwapMethodStrings
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            List<string> inputs = new List<string>();

            for (int i = 0; i < count; i++)
            {
                inputs.Add(Console.ReadLine());
            }

            int[] indexes = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

            Box<string> box = new Box<string>(inputs);

            box.Swap(indexes[0], indexes[1]);

            Console.WriteLine(box);
        }
    }
}
