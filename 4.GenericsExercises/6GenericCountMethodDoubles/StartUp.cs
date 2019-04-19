using System;
using System.Collections.Generic;

namespace _6GenericCountMethodDoubles
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            List<double> inputs = new List<double>();

            for (int i = 0; i < count; i++)
            {
                double input = double.Parse(Console.ReadLine());

                inputs.Add(input);
            }

            Box<double> box = new Box<double>();

            double elementToCompare = double.Parse(Console.ReadLine());

            Console.WriteLine(box.CountGreaterElemnts
                (inputs, elementToCompare));
        }
    }
}
