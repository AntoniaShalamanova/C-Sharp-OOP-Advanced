using System;
using System.Reflection;
using System.Text;

public class Spy
{
    public string RevealPrivateMethods(string className)
    {
        StringBuilder sb = new StringBuilder();

        Type classInfo = Type.GetType(className);

        sb.AppendLine($"All Private Methods of Class: {className}");

        sb.AppendLine($"Base Class: {classInfo.BaseType.Name}");

        var privateMethods = classInfo
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var method in privateMethods)
        {
            sb.AppendLine(method.Name);
        }

        return sb.ToString().TrimEnd();
    }
}
