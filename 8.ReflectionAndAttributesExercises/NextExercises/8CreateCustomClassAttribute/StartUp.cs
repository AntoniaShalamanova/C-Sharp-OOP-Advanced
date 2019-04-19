using System;
using System.Reflection;

namespace _8CreateCustomClassAttribute
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Type type = typeof(Weapon);

            var attribute = type.GetCustomAttribute<CustomClassAttribute>();

            string command = Console.ReadLine();

            while (command != "END")
            {
                switch (command)
                {
                    case "Author":
                        Console.WriteLine($"Author: {attribute.Author}");
                        break;

                    case "Revision":
                        Console.WriteLine($"Revision: {attribute.Revision}");
                        break;

                    case "Description":
                        Console.WriteLine($"Class description: {attribute.Description}");
                        break;

                    case "Reviewers":
                        Console.WriteLine($"Reviewers: {string.Join(", ", attribute.Reviewers)}");
                        break;

                    default:
                        break;
                }

                command = Console.ReadLine();
            }
        }
    }
}
