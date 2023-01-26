using ascender.Enum;

namespace ascender.DomainObjects;

public class DecreasingMetric : Metric
{
    public DecreasingMetric(string name, decimal? max, Direction direction, int window) : base(name, max, direction, window)
    {
    }
}