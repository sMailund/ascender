using System.Transactions;

namespace ascender.DomainObjects;

public class IncreasingMetric : Metric
{
    public IncreasingMetric() 
    {
        Cutoff = decimal.MinValue;
    }

    protected override bool IsBetterThanCutoff(decimal newValue) => Cutoff <= newValue;
    protected override bool IsWithinThreshold(decimal newValue) => newValue >= Threshold;
}