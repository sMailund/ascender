using System.Transactions;
using ascender.Enum;

namespace ascender.DomainObjects;

public class IncreasingMetric : Metric
{
    public IncreasingMetric() 
    {
        
    }
    public IncreasingMetric(string name, decimal? max, Direction direction, int window) : base(name, max, window)
    {
    }
    
    protected override bool IsBetterThanCutoff(decimal newValue) => Cutoff <= newValue;
    
}