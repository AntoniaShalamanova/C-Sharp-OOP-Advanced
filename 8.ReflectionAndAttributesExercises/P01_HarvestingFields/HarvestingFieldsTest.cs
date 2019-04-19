namespace P01_HarvestingFields
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class HarvestingFieldsTest
    {
        public static void Main()
        {
            Type type = typeof(HarvestingFields);

            string command = Console.ReadLine();

            FieldInfo[] fields = type.GetFields(BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance);

            FieldInfo[] fieldsToPrint;

            while (command != "HARVEST")
            {
                if (command != "all")
                {
                    fieldsToPrint = fields
                               .Where(f => f.Attributes
                               .ToString()
                               .ToLower()
                               .Replace("family", "protected") == command)
                               .ToArray();
                }
                else
                {
                    fieldsToPrint = fields;
                }

                foreach (var field in fieldsToPrint)
                {
                    string accessModeifier = field
                        .Attributes
                        .ToString()
                        .ToLower()
                        .Replace("family", "protected");

                    Console.WriteLine($"{accessModeifier} {field.FieldType.Name} {field.Name}");
                }

                command = Console.ReadLine();
            }
        }
    }
}
