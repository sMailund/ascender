using ascender.core.domainEntities;
using ascender.Dto;

namespace ascender.core.repositories;

public interface IMetricRepository
{
    public void CreateMetric(CreateMetricDto dto);
    public void CreateMetric(Metric metric);
    public void MetricCommitted(string name, decimal value);
    public decimal GetCutoff(string name);
    public Metric GetMetric(string name);
}