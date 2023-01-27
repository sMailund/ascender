using ascender.core.domainEntities;
using ascender.Enum;
using Xunit;
using static tests.testData.TestData;

namespace tests.UnitTests.DomainObjects;

public class IncreasingMetricTests
{
    [Fact]
    public void Validate_AboveThresholdAndLowerThanCutoff_ShouldAccept()
    {
        var metric = new IncreasingMetric
        {
            Cutoff = 10,
            Name = AMetricName(Direction.Increase),
            Threshold = 5
        };

        var result = metric.Validate(5);
        Assert.True(result);
    }

    [Fact]
    public void Validate_AboveThresholdAndAboveCutoff_ShouldAccept()
    {
        var metric = new IncreasingMetric
        {
            Cutoff = 7,
            Name = AMetricName(Direction.Increase),
            Threshold = 5
        };

        var result = metric.Validate(7);
        Assert.True(result);
    }
}