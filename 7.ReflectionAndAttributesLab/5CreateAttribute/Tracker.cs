using System;
using System.Reflection;

public class Tracker
{
    public void PrintMethodsByAuthor()
    {
        Type type = typeof(StartUp);

        var methods = type.GetMethods(BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Static);

        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<SoftUniAttribute>();

            if (attribute != null)
            {
                Console.WriteLine($"{method.Name} is writtent by {attribute.Name}");
            }
        }
    }
}