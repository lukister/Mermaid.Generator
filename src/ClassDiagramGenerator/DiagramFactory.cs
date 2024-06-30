using ClassDiagramGenerator.Builders;

namespace ClassDiagramGenerator;

public static class DiagramFactory
{
    public static IDiagramBuilder CreateBuilder() => new DiagramBuilder();
}
