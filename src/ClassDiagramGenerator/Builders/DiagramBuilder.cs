using System.Text;
using ClassDiagramGenerator.Extensions;

namespace ClassDiagramGenerator.Builders;

internal class DiagramBuilder : IDiagramBuilder
{
    private readonly List<Type> _types = [];
    public IDiagramBuilder AddClass(Type type)
    {
        _types.Add(type);
        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();
        foreach (var type in _types)
        {
            builder.WriteClass(type);
        }
        return builder.ToString();
    }
}
