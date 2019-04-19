using System;
using System.Collections.Generic;

namespace _6StrategyPattern
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var nameSorted = new SortedSet<Person>(new NameComparator());
            var ageSorted = new SortedSet<Person>(new AgeComparator());

            Person person;

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string[] inputArgs = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                person = new Person(inputArgs[0], int.Parse(inputArgs[1]));

                nameSorted.Add(person);
                ageSorted.Add(person);
            }

            Console.WriteLine(string.Join(Environment.NewLine, nameSorted));
            Console.WriteLine(string.Join(Environment.NewLine, ageSorted));
        }
    }
}
