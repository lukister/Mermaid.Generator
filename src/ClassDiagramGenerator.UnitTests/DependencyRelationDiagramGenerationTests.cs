using Xunit.Abstractions;

namespace ClassDiagramGenerator.UnitTests;

public class DependencyRelationDiagramGenerationTests
{
    private readonly ITestOutputHelper _testOutput;

    public DependencyRelationDiagramGenerationTests(ITestOutputHelper testOutput)
    {
        _testOutput = testOutput;
    }

    [Fact]
    public void GivenClassDigramWithDependency_WhenClassesWithDependenciesAdded_ThenRelationExists()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(ClassA))
            .AddClass(typeof(ClassB))
            .AddClass(typeof(ClassC))
            .IncludeRelation(RelationType.Dependency)
            .Build();

        _testOutput.WriteLine(builder);

        Assert.Contains("ClassC ..> ClassA", builder);
    }

    [Fact]
    public void GivenClassDigramWithInheritance_WhenClassesWithDependenciesAdded_ThenRelationNotExists()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(ClassA))
            .AddClass(typeof(ClassB))
            .AddClass(typeof(ClassC))
            .IncludeRelation(RelationType.Inheritance)
            .Build();

        _testOutput.WriteLine(builder);

        Assert.DoesNotContain("ClassA ..> ClassC", builder);
    }

    [Fact]
    public void GivenClassDigramWithDependencies_WhenClassesWithInheritanceAdded_ThenRelationNotExists()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass(typeof(ClassA))
            .AddClass(typeof(ClassB))
            .IncludeRelation(RelationType.Dependency)
            .Build();

        _testOutput.WriteLine(builder);

        Assert.DoesNotContain("ClassA ..> ClassC", builder);
    }

    internal class ClassA
    {
        public int Id { get; set; }
    }

    internal class ClassB : ClassA
    {
        public string Name { get; set; } = string.Empty;
    }

    internal class ClassC
    {
        public ClassC(ClassA classA)
        {
            ClassA = classA;
        }

        public ClassA ClassA { get; }
    }
}