using ascender.DomainObjects;
using ascender.Enum;

namespace ascender.Factory;

public class MetricFactory
{
    private Metric _metric;

    private MetricFactory()
    {
        
    }

    public MetricFactory Init(string name, Direction direction, decimal cutoff, int window)
    {
        _metric = direction == Direction.Increase ? new IncreasingMetric() : new DecreasingMetric();
        _metric.Name = name;
        _metric.Cutoff = cutoff;
        _metric.Window = window;
        return this;
    }

    public MetricFactory WithMax(decimal value)
    {
        _metric.Max = value;
        return this;
    }

    public Metric Build()
    {
        return _metric;
    }

}