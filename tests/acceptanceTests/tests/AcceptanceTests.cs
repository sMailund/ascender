using System.Threading.Tasks;
using ascender.Dto;
using ascender.Enum;
using Microsoft.AspNetCore.Mvc.Testing;
using tests.acceptanceTests.drivers;
using Xunit;
using static tests.testData.TestData;

namespace tests.acceptanceTests.tests;

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

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 10
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

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 1
        };
        
        await metrics.CreateMetric(dto);
        
        await metrics.CommitEntry(metricName, 5);
        var result = await metrics.ValidateEntry(metricName, 3);

        Assert.False(result);
    }
    
    [Fact]
    public async Task AcceptLowerThanPreviousHigherThanCutoff()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 10
        };
        
        await metrics.CreateMetric(dto);
        await metrics.CommitEntry(metricName, 5);
        
        var result1 = await metrics.ValidateEntry(metricName, 10);
        Assert.True(result1);
        await metrics.CommitEntry(metricName, 10);
        
        var result = await metrics.ValidateEntry(metricName, 7);
        Assert.True(result);
    }
    
    [Fact]
    public async Task ShouldSetCutoffToLowestValueInWindow()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 3
        };
        
        await metrics.CreateMetric(dto);
        await metrics.CommitEntry(metricName, 5);
        
        await metrics.CommitEntry(metricName, 10);
        await metrics.CommitEntry(metricName, 15);
        await metrics.CommitEntry(metricName, 12);
        
        var result1 = await metrics.ValidateEntry(metricName, 10);
        Assert.True(result1);
        
        var result = await metrics.ValidateEntry(metricName, 9);
        Assert.False(result);
    }

    [Fact]
    public async Task ShouldHandleMetricsWithDecimalPoints()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 3
        };
        
        await metrics.CreateMetric(dto);
        await metrics.CommitEntry(metricName, 10.05m);
        
        await metrics.CommitEntry(metricName, 10.10m);
        await metrics.CommitEntry(metricName, 10.15m);
        await metrics.CommitEntry(metricName, 10.12m);
        
        var result1 = await metrics.ValidateEntry(metricName, 10.10m);
        Assert.True(result1);
        
        var result = await metrics.ValidateEntry(metricName, 10.09m);
        Assert.False(result);
    }

    [Fact]
    public async Task ShouldRejectMetricsOutsideOfMaximum()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 3,
            Max = 10
        };
        
        await metrics.CreateMetric(dto);
        
        var result1 = await metrics.ValidateEntry(metricName, 10);
        Assert.True(result1);
       
        var result2 = await metrics.ValidateEntry(metricName, 10.01m);
        Assert.False(result2);
    }
    
    [Fact]
    public async Task ShouldRejectMetricsOutsideOfMinimum()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 3,
            Minimum = 10
        };
        
        await metrics.CreateMetric(dto);
        
        var result1 = await metrics.ValidateEntry(metricName, 10);
        Assert.True(result1);
       
        var result2 = await metrics.ValidateEntry(metricName, 9.99m);
        Assert.False(result2);
    }
    
    [Fact]
    public async Task RejectHigherValue()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Decrease);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Decrease,
            Window = 1
        };
        
        await metrics.CreateMetric(dto);
        
        await metrics.CommitEntry(metricName, 5);
        var result = await metrics.ValidateEntry(metricName, 8);

        Assert.False(result);
    }
    
    [Fact]
    public async Task AcceptLowerValueAboveThreshold()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Increase);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Increase,
            Window = 3,
            Threshold = 100
        };
        
        await metrics.CreateMetric(dto);
        
        await metrics.CommitEntry(metricName, 105);
        await metrics.CommitEntry(metricName, 108);
        await metrics.CommitEntry(metricName, 110);
        
        var result1 = await metrics.ValidateEntry(metricName, 100);
        Assert.True(result1);
        
        var result2 = await metrics.ValidateEntry(metricName, 99);
        Assert.False(result2);
    }
    
    [Fact]
    public async Task AcceptLowerValueBelowThreshold()
    {
        var client = _factory.CreateClient();
        var metrics = new MetricsDriver(client);

        var metricName = AMetricName(Direction.Decrease);
        
        var dto = new CreateMetricDto
        {
            Name = metricName,
            Direction = Direction.Decrease,
            Window = 3,
            Threshold = 20
        };
        
        await metrics.CreateMetric(dto);
        
        await metrics.CommitEntry(metricName, 15);
        await metrics.CommitEntry(metricName, 12);
        await metrics.CommitEntry(metricName, 10);
        
        var result1 = await metrics.ValidateEntry(metricName, 20);
        Assert.True(result1);
        
        var result2 = await metrics.ValidateEntry(metricName, 21);
        Assert.False(result2);
    }

}