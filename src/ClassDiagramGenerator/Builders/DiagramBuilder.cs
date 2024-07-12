using System.Text;
using ClassDiagramGenerator.Extensions;

namespace ClassDiagramGenerator.Builders;

internal class DiagramBuilder : IDiagramBuilder
{
    private readonly List<Type> _types = [];
    private readonly List<RelationType> _relationTypes = [];
    public IDiagramBuilder AddClass(Type type)
    {
        _types.Add(type);
        return this;
    }

    public IDiagramBuilder IncludeRelation(RelationType relationType)
    {
        if (_relationTypes.Contains(relationType))
            return this;
        _relationTypes.Add(relationType);
        return this;
    }

    public string Build()
    {
        var builder = new StringBuilder();
        builder.AppendLine("classDiagram");
        foreach (var type in _types)
        {
            builder.WriteClass(type);
        }
        foreach (var relationType in _relationTypes)
        {
            switch (relationType)
            {
                case RelationType.Inheritance:
                    WriteInheritanceRelations(builder);
                    break;
                case RelationType.Dependency:
                    WriteDependenciesRelations(builder);
                    break;
            }
        }
        return builder.ToString();
    }

    private void WriteInheritanceRelations(StringBuilder builder)
    {
        for (int i = 0; i < _types.Count; i++)
            for (int j = 0; j < _types.Count; j++)
            {
                if (i == j)
                    continue; 

                if (_types[i].IsAssignableFrom(_types[j]))
                    builder.AppendLine($"\t{_types[i].Name} <|-- {_types[j].Name}");
            }
    }

    private void WriteDependenciesRelations(StringBuilder builder)
    {
        foreach (var type in _types)
        {
            var constructors = type.GetConstructors();
            foreach (var constrictor in constructors)
            {
                var parameters = constrictor.GetParameters()
                    .Select(x => x.ParameterType)
                    .ToList();

                var intersecTypes = _types.Intersect(parameters);
                foreach(var intersecType in intersecTypes)
                    builder.AppendLine($"\t{type.Name} ..> {intersecType.Name}");
            }
        }
    }
}
