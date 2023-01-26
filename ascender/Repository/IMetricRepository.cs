using ascender.Dto;

namespace ascender.Repository;

public interface IMetricRepository
{
    public void CreateMetric(CreateMetricDto dto);
    public void MetricCommitted(string name, decimal value);
    public decimal GetCutoff(string name);
    public decimal? GetMaximum(string name);
    public CreateMetricDto GetMetric(string name);
}