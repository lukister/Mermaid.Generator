
namespace ClassDiagramGenerator;

public interface IDiagramBuilder
{
    public IDiagramBuilder AddClass(Type type);
    string Build();
}