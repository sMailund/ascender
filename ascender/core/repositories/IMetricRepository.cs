using ascender.core.domainEntities;

namespace ascender.core.repositories;

public interface IMetricRepository
{
    public void CreateMetric(Metric metric);
    public void MetricCommitted(string name, decimal value);
    public decimal GetCutoff(string name);
    public Metric GetMetric(string name);
}