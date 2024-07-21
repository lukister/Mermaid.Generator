
namespace ClassDiagramGenerator;

public interface IDiagramBuilder
{
    public IDiagramBuilder AddClass(Type type);
    public IDiagramBuilder AddClass<T>() => AddClass(typeof(T));
    public IDiagramBuilder IncludeRelation(RelationType relationType);
    string Build();
}
