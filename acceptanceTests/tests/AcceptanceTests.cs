using System.Threading.Tasks;
using acceptanceTests.drivers;
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
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = "test";
        
        var dto = new CreateMetricDto()
        {
            Name = metricName,
            Type = MetricType.Number,
            Direction = Direction.Increase
        };
        
        await metrics.CreateMetric(dto);
        await metrics.CommitEntry(metricName, 0);
        var result = await metrics.ValidateEntry(metricName, 1);

        Assert.True(result);
    }
    
    [Fact]
    public async Task RejectLowerValue()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);
        var time = new TimeDriver(client);

        var metricName = "test2";
        
        var dto = new CreateMetricDto()
        {
            Name = metricName,
            Type = MetricType.Number,
            Direction = Direction.Increase
        };
        
        await metrics.CreateMetric(dto);
        await time.AddDays(10);
        
        await metrics.CommitEntry(metricName, 5);
        var result = await metrics.ValidateEntry(metricName, 3);

        Assert.False(result);
    }
    
    [Fact]
    public async Task AcceptLowerThanPreviousHigherThanCutoff()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);
        var time = new TimeDriver(client);

        var metricName = "test3";
        
        var dto = new CreateMetricDto()
        {
            Name = metricName,
            Type = MetricType.Number,
            Direction = Direction.Increase
        };
        
        await metrics.CreateMetric(dto);
        await metrics.CommitEntry(metricName, 5);
        
        await time.AddDays(3);
        var result1 = await metrics.ValidateEntry(metricName, 10);
        Assert.True(result1);
        await metrics.CommitEntry(metricName, 10);
        
        await time.AddDays(3);
        var result = await metrics.ValidateEntry(metricName, 7);
        Assert.True(result);
    }
    
}