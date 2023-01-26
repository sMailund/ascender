namespace ascender.DomainObjects;

public class DecreasingMetric : Metric
{
    public DecreasingMetric()
    {
        Cutoff = decimal.MaxValue;
    }

    protected override bool IsBetterThanCutoff(decimal newValue) => Cutoff >= newValue;
}