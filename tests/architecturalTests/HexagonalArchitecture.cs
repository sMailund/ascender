using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using Assembly = System.Reflection.Assembly;

namespace tests.architecturalTests;

public class HexagonalArchitecture
{
    private static readonly Architecture Architecture =
        new ArchLoader().LoadAssemblies(Assembly.Load("ascender")).Build();

    private readonly IObjectProvider<IType> Core =
        Classes().That().ResideInNamespace("core.*", true).As("core");

    private readonly IObjectProvider<IType> DomainEntities =
        Classes().That().ResideInNamespace("core.domainEntities.*", true).As("domain entities");
    
    private readonly IObjectProvider<IType> Repositories =
        Classes().That().ResideInNamespace("core.repositories.*", true).As("repositories");
    
    private readonly IObjectProvider<IType> Services =
        Classes().That().ResideInNamespace("core.services.*", true).As("services");
    
    private readonly IObjectProvider<IType> NotCore =
        Classes().That().DoNotResideInNamespace("core.*", true).As("not core");


    [Fact]
    public void ShouldUseHexagonalArchitecture()
    {
        Classes().That().Are(Core).Should().NotDependOnAny(NotCore).Check(Architecture);
        Classes().That().Are(DomainEntities)
            .Should().NotDependOnAny(Repositories)
            .OrShould().NotDependOnAny(Services)
            .Check(Architecture);
    }
    
}