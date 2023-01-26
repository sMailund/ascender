using ascender.Repository;

namespace ascender.Services;

public class MetricService
{
    
    private readonly IMetricRepository _repo;

    public MetricService(IMetricRepository repo)
    {
        _repo = repo;
    }
}