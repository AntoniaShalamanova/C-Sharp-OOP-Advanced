using System;
using System.Collections.Generic;

namespace _5GenericCountMethodStrings
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

            Box<string> box = new Box<string>();

            Console.WriteLine(box.CountGreaterElemnts
                (inputs, Console.ReadLine()));
        }
    }
}
