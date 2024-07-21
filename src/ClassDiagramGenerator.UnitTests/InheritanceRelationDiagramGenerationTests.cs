using Xunit.Abstractions;

namespace ClassDiagramGenerator.UnitTests;

public class InheritanceRelationDiagramGenerationTests
{
    private readonly ITestOutputHelper _testOutput;

    public InheritanceRelationDiagramGenerationTests(ITestOutputHelper testOutput)
    {
        _testOutput = testOutput;
    }

    [Fact]
    public void GivenClassDigramWithInheritation_WhenClassesWithInheritationAdded_ThenRelationExists()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass<ClassA>()
            .AddClass<ClassB>()
            .AddClass<ClassC>()
            .IncludeRelation(RelationType.Inheritance)
            .Build();

        _testOutput.WriteLine(builder);

        Assert.Contains("ClassA <|-- ClassB", builder);
    }

    [Fact]
    public void GivenClassDigramWithDependencies_WhenClassesWithInheritationAdded_ThenRelationNotExists()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass<ClassA>()
            .AddClass<ClassB>()
            .AddClass<ClassC>()
            .IncludeRelation(RelationType.Dependency)
            .IncludeRelation(RelationType.Inheritance)
            .Build();

        _testOutput.WriteLine(builder);

        Assert.DoesNotContain("ClassA <|-- ClassB", builder);
    }

    [Fact]
    public void GivenClassDigramWithInheritance_WhenClassesWithDependenciesAdded_ThenRelationNotExists()
    {
        string builder = DiagramFactory.CreateBuilder()
            .AddClass<ClassA>()
            .AddClass<ClassC>()
            .IncludeRelation(RelationType.Inheritance)
            .Build();

        _testOutput.WriteLine(builder);

        Assert.DoesNotContain("ClassA <|-- ClassB", builder);
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