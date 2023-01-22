using System.Threading.Tasks;
using acceptanceTests.driver;
using ascender.Dto;
using ascender.Enum;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace acceptanceTests.tests;

public class AcceptanceTests : IClassFixture<WebApplicationFactory<ascender.Program>>
{
    
    private readonly WebApplicationFactory<ascender.Program> _factory;
    

    public AcceptanceTests(WebApplicationFactory<ascender.Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CommitAndValidate()
    {
        // Arrange
        var client = _factory.CreateClient();
        var driver = new HttpDriver(client);

        var metricName = "test";
        
        var dto = new CreateMetricDto()
        {
            Name = metricName,
            Type = MetricType.Number,
            Direction = Direction.Increase
        };
        
        await driver.CreateMetric(dto);
        await driver.CommitEntry(metricName, 0);
        var result = await driver.ValidateEntry(metricName, 1);

        Assert.True(result);
    }
    
    [Fact]
    public async Task RejectLowerValue()
    {
        // Arrange
        var client = _factory.CreateClient();
        var driver = new HttpDriver(client);

        var metricName = "test2";
        
        var dto = new CreateMetricDto()
        {
            Name = metricName,
            Type = MetricType.Number,
            Direction = Direction.Increase
        };
        
        await driver.CreateMetric(dto);
        await driver.AddDays(10);
        
        await driver.CommitEntry(metricName, 5);
        var result = await driver.ValidateEntry(metricName, 3);

        Assert.False(result);
    }
    
    [Fact]
    public async Task AcceptLowerThanPreviousHigherThanCutoff()
    {
        // Arrange
        var client = _factory.CreateClient();
        var driver = new HttpDriver(client);

        var metricName = "test3";
        
        var dto = new CreateMetricDto()
        {
            Name = metricName,
            Type = MetricType.Number,
            Direction = Direction.Increase
        };
        
        await driver.CreateMetric(dto);
        await driver.CommitEntry(metricName, 5);
        
        await driver.AddDays(3);
        var result1 = await driver.ValidateEntry(metricName, 10);
        Assert.True(result1);
        await driver.CommitEntry(metricName, 10);
        
        await driver.AddDays(3);
        var result = await driver.ValidateEntry(metricName, 7);
        Assert.True(result);
    }
    
}