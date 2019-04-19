using System;
using System.Linq;
using System.Reflection;
using System.Text;

public class Spy
{
    public string AnalyzeAcessModifiers(string className)
    {
        StringBuilder sb = new StringBuilder();

        Type classInfo = Type.GetType(className);

        var fields = classInfo.GetFields();

        var privateMethods = classInfo
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.Name.StartsWith("get"));

        var publicMethods = classInfo
           .GetMethods(BindingFlags.Public | BindingFlags.Instance)
           .Where(m => m.Name.StartsWith("set"));

        foreach (var fieldInfo in fields)
        {
            sb.AppendLine($"{fieldInfo.Name} must be private!");
        }

        foreach (var methodInfo in privateMethods)
        {
            sb.AppendLine($"{methodInfo.Name} have to be public!");
        }

        foreach (var methodInfo in publicMethods)
        {
            sb.AppendLine($"{methodInfo.Name} have to be private!");
        }

        return sb.ToString().TrimEnd();
    }
}
