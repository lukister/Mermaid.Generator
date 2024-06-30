using System.Reflection;
using System.Text;
using System.Xml.Schema;

namespace ClassDiagramGenerator.Extensions;

internal static class ClassDiagramExtensions
{
    public static HashSet<string> defaultObjectMethods = typeof(object).GetMethods().Select(x => x.Name).ToHashSet();
    public static void WriteClass(this StringBuilder builder, Type type)
    {
        builder.Append("\tclass ");
        builder.Append(type.Name);
        builder.Append("{");
        builder.AppendLine();

        foreach (var method in type.GetMethods().Where(x => x.IsPublic && !x.IsSpecialName))
        {
            if (defaultObjectMethods.Contains(method.Name))
                continue;
            builder.AppendLine($"\t\t{FullMetodFormat(method)}");
        }

        foreach (var property in type.GetProperties())
        {
            builder.AppendLine($"\t\t{FullMetodFormat(property)}");
        }

        builder.AppendLine("\t}");
    }

    private static string FullMetodFormat(MethodInfo method)
    {
        var tmp = method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}");
        return $"{method.Name}({string.Join(", ", tmp)}) {method.ReturnType.Name}";
    }

    private static string FullMetodFormat(PropertyInfo property)
    {
        return $"{property.PropertyType.Name} {property.Name}";
    }
}
