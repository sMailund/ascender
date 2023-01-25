using ascender.Dto;
using ascender.Enum;
using ascender.Validators;
using static acceptanceTests.testData.TestData;
using Xunit;

namespace acceptanceTests.unitTests;

public class CreateMetricDtoValidatorTests
{
    private CreateMetricDtoValidator _validator = new();

    [Fact]
    public void ShouldAcceptValidDtoWithPositiveNumber()
    {
        var dto = new CreateMetricDto
        {
            Name = AMetricName(Direction.Increase),
            Direction = Direction.Increase,
            Window = 10
        };
        
        Assert.True(_validator.Validate(dto));
    }
    
    [Fact]
    public void ShouldAcceptValidDtoWithNegativeNumber()
    {
        var dto = new CreateMetricDto
        {
            Name = AMetricName(Direction.Increase),
            Direction = Direction.Increase,
            Window = -1
        };
        
        Assert.False(_validator.Validate(dto));
    }
    
    [Fact]
    public void ShouldAcceptValidDtoWithZeroWindow()
    {
        var dto = new CreateMetricDto
        {
            Name = AMetricName(Direction.Increase),
            Direction = Direction.Increase,
            Window = 0
        };
        
        Assert.False(_validator.Validate(dto));
    }
}