using ascender.DomainObjects;
using ascender.Enum;
using Xunit;
using static tests.testData.TestData;

namespace tests.UnitTests.DomainObjects;

public class DecreasingMetricTests
{
    [Fact]
    public void Validate_BelowThresholdAndAboveCutoff_ShouldAccept()
    {
        var metric = new DecreasingMetric()
        {
            Cutoff = 10,
            Name = AMetricName(Direction.Decrease),
            Threshold = 20
        };

        var result = metric.Validate(20);
        Assert.True(result);
    }

    [Fact]
    public void Validate_BelowThresholdAndBelowCutoff_ShouldAccept()
    {
        var metric = new DecreasingMetric()
        {
            Cutoff = 7,
            Name = AMetricName(Direction.Increase),
            Threshold = 20
        };

        var result = metric.Validate(7);
        Assert.True(result);
    }
}
