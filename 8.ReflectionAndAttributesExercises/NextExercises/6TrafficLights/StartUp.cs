using System;
using System.Linq;
using System.Reflection;

namespace _6TrafficLights
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] lights =Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                ChangeLights(lights);

                Console.WriteLine(string.Join(" ", lights));
            }
        }

        private static void ChangeLights(string[] lights)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                string light = lights[i];

                Assembly assembly = Assembly.GetExecutingAssembly();

                Type type = assembly.GetTypes().First(t => t.Name == light);

                var instance = Activator.CreateInstance(type);

                var field = type.GetField("ChangeLingt");

                lights[i] =  field.GetValue(instance).ToString();
            }
        }
    }
}
