using ascender.Dto;
using ascender.Enum;
using ascender.Validators;
using static acceptanceTests.testData.TestData;
using Xunit;

namespace acceptanceTests.unitTests;

public class EntryDtoValidatorTests
{
    private EntryDtoValidator _validator = new();
    
    [Fact]
    public void ShouldAcceptValidDtoWithValueWithoutDecimals()
    {
        var dto = new EntryDto
        {
            Name = AMetricName(Direction.Increase),
            Value = 10
        };
        
        Assert.True(_validator.Validate(dto));
    }
    
    [Fact]
    public void ShouldAcceptValidDtoWithValueWithOneDecimal()
    {
        var dto = new EntryDto
        {
            Name = AMetricName(Direction.Increase),
            Value = 10.0m
        };
        
        Assert.True(_validator.Validate(dto));
    }
    
    [Fact]
    public void ShouldAcceptValidDtoWithValueWithTwoDecimals1()
    {
        var dto = new EntryDto
        {
            Name = AMetricName(Direction.Increase),
            Value = 10.10m
        };
        
        Assert.True(_validator.Validate(dto));
    }
    
    [Fact]
    public void ShouldAcceptValidDtoWithValueWithTwoDecimals2()
    {
        var dto = new EntryDto
        {
            Name = AMetricName(Direction.Increase),
            Value = 1.22m
        };
        
        Assert.True(_validator.Validate(dto));
    }
    
    [Fact]
    public void ShouldRejectDtoWithThreeDecimals()
    {
        var dto = new EntryDto
        {
            Name = AMetricName(Direction.Increase),
            Value = 1.222m
        };
        
        Assert.False(_validator.Validate(dto));
    }
}