using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.xUnit;
using ascender.Services;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace tests.architecturalTests;

public class ArchitecturalTests
{
    
    private static readonly Architecture Architecture =
        new ArchLoader().LoadAssemblies(System.Reflection.Assembly.Load("ascender")).Build();
    
    [Fact]
    public void ControllersShouldAccesRepositoriesThroughServices()
    {
        var rule = Classes().That().ResideInNamespace("ascender.Controllers")
            .Should().NotDependOnAnyTypesThat().ResideInNamespace("ascender.Repository")
            .Because("Controllers should access repositories through services");
        
        rule.Check(Architecture);
    }
}