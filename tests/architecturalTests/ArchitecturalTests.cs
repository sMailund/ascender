using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using Assembly = System.Reflection.Assembly;

namespace tests.architecturalTests;

public class ArchitecturalTests
{
    
    private static readonly Architecture Architecture =
        new ArchLoader().LoadAssemblies(Assembly.Load("ascender")).Build();
    
    [Fact]
    public void ControllersShouldAccessRepositoriesThroughServices()
    {
        var rule = Classes().That().ResideInNamespace("ascender.Controllers")
            .Should().NotDependOnAnyTypesThat().ResideInNamespace("ascender.Repository")
            .Because("Controllers should access repositories through services");
        
        rule.Check(Architecture);
    }

    [Fact]
    public void NoCycles()
    {
        SliceRuleDefinition
            .Slices().Matching("(*)")
            .Should().BeFreeOfCycles()
            .Check(Architecture);
    }
}