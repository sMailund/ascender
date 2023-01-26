using System.Transactions;

namespace ascender.DomainObjects;

public class IncreasingMetric : Metric
{
    public IncreasingMetric() 
    {
        
    }

    protected override bool IsBetterThanCutoff(decimal newValue) => Cutoff <= newValue;
    
}