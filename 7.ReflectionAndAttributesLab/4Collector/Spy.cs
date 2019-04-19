using System;
using System.Linq;
using System.Reflection;
using System.Text;

public class Spy
{
    public string CollectGettersAndSetters(string className)
    {
        StringBuilder sb = new StringBuilder();

        Type classInfo = Type.GetType(className);

        var getters = classInfo
            .GetMethods(BindingFlags.NonPublic | 
            BindingFlags.Instance |
            BindingFlags.Public | 
            BindingFlags.Static)
            .Where(m => m.Name.StartsWith("get"));

        var setters = classInfo
           .GetMethods(BindingFlags.NonPublic |
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.Static)
           .Where(m => m.Name.StartsWith("set"));

        foreach (var getter in getters)
        {
            sb.AppendLine($"{getter.Name} will return {getter.ReturnType}");
        }

        foreach (var setter in setters)
        {
            sb.AppendLine($"{setter.Name} will set field of {setter.GetParameters().First().ParameterType}");
        }

        return sb.ToString().TrimEnd();
    }
}
