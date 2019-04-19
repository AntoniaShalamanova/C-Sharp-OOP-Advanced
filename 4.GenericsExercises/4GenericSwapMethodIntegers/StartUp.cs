using System;
using System.Collections.Generic;
using System.Linq;

namespace _4GenericSwapMethodIntegers
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            List<int> inputs = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int input = int.Parse(Console.ReadLine());

                inputs.Add(input);
            }

            int[] indexes = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

            Box<int> box = new Box<int>(inputs);

            box.Swap(indexes[0], indexes[1]);

            Console.WriteLine(box);
        }
    }
}
