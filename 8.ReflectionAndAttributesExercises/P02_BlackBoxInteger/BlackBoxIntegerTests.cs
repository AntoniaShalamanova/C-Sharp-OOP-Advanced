namespace P02_BlackBoxInteger
{
    using System;
    using System.Reflection;

    public class BlackBoxIntegerTests
    {
        public static void Main()
        {
            Type type = typeof(BlackBoxInteger);

            var instance = (BlackBoxInteger)Activator
                .CreateInstance(type, true);

            FieldInfo innerValue = type.GetField("innerValue",
                BindingFlags.NonPublic | BindingFlags.Instance);

            string input = Console.ReadLine();

            while (input != "END")
            {
                string[] inputArgs = input
                    .Split('_', StringSplitOptions.RemoveEmptyEntries);

                string command = inputArgs[0];
                int value = int.Parse(inputArgs[1]);

                var method = type.GetMethod(command, 
                    BindingFlags.NonPublic|BindingFlags.Instance);

                method.Invoke(instance, new object[] { value });

                Console.WriteLine(innerValue.GetValue(instance));

                input = Console.ReadLine();
            }
        }
    }
}
