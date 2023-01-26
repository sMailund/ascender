namespace ascender.DomainObjects;

public class DecreasingMetric : Metric
{
    public DecreasingMetric()
    {
        
    }
    
    public DecreasingMetric(string name, decimal? max, int window) : base(name, max, window)
    {
    }

    protected override bool IsBetterThanCutoff(decimal newValue) => Cutoff >= newValue;
}