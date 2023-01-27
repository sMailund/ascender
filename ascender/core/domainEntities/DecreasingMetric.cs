namespace ascender.core.domainEntities;

public class DecreasingMetric : Metric
{
    public DecreasingMetric()
    {
        Cutoff = decimal.MaxValue;
    }

    protected override bool IsBetterThanCutoff(decimal newValue) => Cutoff >= newValue;
    protected override bool IsWithinThreshold(decimal newValue) => newValue <= Threshold;
}