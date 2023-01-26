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
    
    public bool Validate(string metricName, EntryDto dto) // TODO useless dto
    {
        var metric = _repo.GetMetric(metricName);
        var value = _repo.GetCutoff(metricName);
        
        var max = metric.Max;
        var withInRange = !max.HasValue || dto.Value <= max;
        var betterThanCutoff = metric.Direction == Direction.Increase ? value <= dto.Value : value >= dto.Value;

        return betterThanCutoff && withInRange;
    }
}