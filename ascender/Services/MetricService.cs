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
        var metric = new Metric(dto.Name, dto.Max, dto.Direction, dto.Window);
        _repo.CreateMetric(metric);
    }
    
    public bool Validate(string metricName, EntryDto dto)
    {
        var metric = _repo.GetMetric(metricName);
        return metric.Validate(dto.Value);
    }
    
    public void Commit(string metricName, EntryDto dto) 
    {
        if (!Validate(metricName, dto))
        {
            throw new ArgumentException();
        }
        _repo.MetricCommitted(metricName, dto.Value);
    }
}