namespace ClassDiagramGenerator.UnitTests;

public class BaseDiagramGenerationTests
{
    [Fact]
    public void GivenDefaultBuilder_WhenOneClassAdded_ThenDiagramIsNotEmpty()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(Class1))
            .Build();

        Assert.NotEmpty(builder);
    }

    private class Class1
    {
        public string Name { get; set; }
    }
}