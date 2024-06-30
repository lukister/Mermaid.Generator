using System.Reflection;
using System.Text;

namespace ClassDiagramGenerator.Extensions;

internal static class ClassDiagramExtensions
{
    public static void WriteClass(this StringBuilder builder, Type type)
    {
        builder.Append("\t");
        builder.Append(type.Name);
        builder.Append("{");
        builder.AppendLine();

        foreach (var method in type.GetMethods())
        {
            WriteMethod(builder, method);
        }

        builder.AppendLine("}");
    }

    private static void WriteMethod(StringBuilder builder, MethodInfo method)
    {
        builder.Append("\t\t");
        builder.Append(method.ReturnType.Name);
        builder.Append(" ");
        builder.AppendLine();
    }

    private static string FullMetodFormat(MethodInfo method)
    {
        var tmp = method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}");
        return $"{method.Name}({string.Join(", ", tmp)}) {method.ReturnType.Name}";
    }
}
