using System;
using System.Collections.Generic;

namespace _5ComparingObjects
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            Person person;

            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] inputArgs = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = inputArgs[0];
                string age = inputArgs[1];
                string town = inputArgs[2];

                person = new Person(name, age, town);

                people.Add(person);

                input = Console.ReadLine();
            }

            int personNumber = int.Parse(Console.ReadLine());

            person = people[personNumber - 1];

            int equalCount = 0;

            foreach (var currentPerson in people)
            {
                if (person.CompareTo(currentPerson) == 0)
                {
                    equalCount++;
                }
            }

            if (equalCount > 1)
            {
                Console.WriteLine("{0} {1} {2}",
                equalCount,
                people.Count - equalCount,
                people.Count);
            }
            else
            {
                Console.WriteLine("No matches");
                return;
            }
        }
    }
}
