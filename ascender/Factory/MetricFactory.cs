using ascender.DomainObjects;
using ascender.Enum;

namespace ascender.Factory;

public class MetricFactory
{
    private Metric _metric;

    public MetricFactory()
    {
    }

    public MetricFactory Init(string name, Direction direction, int window)
    {
        _metric = direction == Direction.Increase ? new IncreasingMetric() : new DecreasingMetric();
        _metric.Name = name;
        _metric.Window = window;
        return this;
    }

    public MetricFactory WithMax(decimal? value)
    {
        _metric.Max = value;
        return this;
    }

    public MetricFactory WithMin(decimal? value)
    {
        _metric.Minimum = value;
        return this;
    }

    public MetricFactory WithThreshold(decimal? value)
    {
        _metric.Threshold = value;
        return this;
    }

    public Metric Build()
    {
        return _metric;
    }
}