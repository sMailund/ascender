using ascender.Dto;

namespace ascender.Repository;

public interface IMetricRepository
{
    public void CreateMetric(CreateMetricDto dto);
    public void MetricCommitted(string name, int value, DateTime time);
    public int GetCutoff(string name);
}