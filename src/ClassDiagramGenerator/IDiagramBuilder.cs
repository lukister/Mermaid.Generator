
namespace ClassDiagramGenerator;

public interface IDiagramBuilder
{
    public IDiagramBuilder AddClass(Type type);
    public IDiagramBuilder IncludeRelation(RelationType relationType);
    string Build();
}
