namespace ascender.DomainObjects;

public class DecreasingMetric : Metric
{
    public DecreasingMetric()
    {
        
    }

    protected override bool IsBetterThanCutoff(decimal newValue) => Cutoff >= newValue;
}