using ascender.Dto;

namespace ascender.Repository;

public interface IMetricRepository
{
    public void CreateMetric(CreateMetricDto dto);
    public void MetricCommitted(string name, int value);
    public int GetCutoff(string name);
}