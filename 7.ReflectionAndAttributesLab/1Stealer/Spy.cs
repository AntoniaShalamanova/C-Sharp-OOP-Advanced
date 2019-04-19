using System;
using System.Reflection;
using System.Text;

public class Spy
{
    public string StealFieldInfo(string className, params string[] fieldsNames)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Class under investigation: {className}");

        Type classInfo = Type.GetType(className);

        var classInstance = Activator.CreateInstance(classInfo);

        foreach (var field in fieldsNames)
        {
            var fieldInfo = classInfo.GetField(field,
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Instance |
                BindingFlags.Static);

            var value = fieldInfo.GetValue(classInstance);

            sb.AppendLine($"{fieldInfo.Name} = {value}");
        }

        return sb.ToString().TrimEnd();
    }
}
