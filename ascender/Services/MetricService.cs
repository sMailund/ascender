using ascender.DomainObjects;
using ascender.Dto;
using ascender.Enum;
using ascender.Repository;

namespace ascender.Services;

public class MetricService
{
    
    private readonly IMetricRepository _repo;

    public MetricService(IMetricRepository repo)
    {
        _repo = repo;
    }
    
    public void CreateNewMetric(CreateMetricDto dto)
    {
        Metric metric = dto.Direction == Direction.Increase
            ? new IncreasingMetric(dto.Name, dto.Max, dto.Direction, dto.Window)
            : new DecreasingMetric(dto.Name, dto.Max, dto.Direction, dto.Window);
        
        _repo.CreateMetric(metric);
    }
    
    public bool Validate(string metricName, decimal value)
    {
        var metric = _repo.GetMetric(metricName);
        return metric.Validate(value);
    }
    
    public void Commit(string metricName, decimal value) 
    {
        if (!Validate(metricName, value))
        {
            throw new ArgumentException();
        }
        _repo.MetricCommitted(metricName, value);
    }
}