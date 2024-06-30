using Xunit.Abstractions;

namespace ClassDiagramGenerator.UnitTests;

public class BaseDiagramGenerationTests
{
    private readonly ITestOutputHelper _testOutput;

    public BaseDiagramGenerationTests(ITestOutputHelper testOutput)
    {
        _testOutput = testOutput;
    }

    [Fact]
    public void GivenDefaultBuilder_WhenOneClassAdded_ThenDiagramIsNotEmpty()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(Class1))
            .Build();

        _testOutput.WriteLine(builder);

        Assert.NotEmpty(builder);
    }

    [Fact]
    public void GivenDefaultBuilder_WhenOneClassAdded_ThenClassNameIsOnDiagram()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(Class1))
            .Build();

        _testOutput.WriteLine(builder);

        Assert.Contains(nameof(Class1), builder);
    }

    [Fact]
    public void GivenDefaultBuilder_WhenClassHasProperty_ThenPropertyNameIsIncludedInDiagram()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(Class1))
            .Build();

        _testOutput.WriteLine(builder);

        Assert.Contains(nameof(Class1.TestName), builder);
    }

    [Fact]
    public void GivenDefaultBuilder_WhenClassHasMethod_ThenMethodNameIsIncludedInDiagram()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(Class1))
            .Build();

        _testOutput.WriteLine(builder);

        Assert.Contains(nameof(Class1.Count), builder);
    }

    [Fact]
    public void GivenDefaultBuilder_WhenClassAdedTodiagram_ThenDefaultMethodsAreNotOnDiagram()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(Class1))
            .Build();

        _testOutput.WriteLine(builder);

        Assert.DoesNotContain(nameof(GetType), builder);
        Assert.DoesNotContain(nameof(ToString), builder);
        Assert.DoesNotContain(nameof(GetHashCode), builder);
        Assert.DoesNotContain(nameof(Equals), builder);
    }

    private class Class1
    {
        public string TestName { get; set; } = string.Empty;
        public int Count(int a) => a + 1;
    }
}