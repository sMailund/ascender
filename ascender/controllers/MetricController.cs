using ascender.Dto;
using ascender.Enum;
using ascender.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ascender.controllers;

[ApiController]
[Route("[controller]")]
public class MetricController : ControllerBase
{
    private readonly IMetricRepository _repo;

    public MetricController(IMetricRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public string Post([FromBody] CreateMetricDto dto)
    {
        _repo.CreateMetric(dto);
        return dto.Name;
    }

    [HttpPost]
    [Route("{metricName}/commit")]
    public void Commit(string metricName, [FromBody] EntryDto dto) // TODO useless dto
    {
        if (!Validate(metricName, dto))
        {
            BadRequest();
        }
        _repo.MetricCommitted(metricName, dto.Value);
    }
    
    [HttpPost]
    [Route("{metricName}/validate")]
    public bool Validate(string metricName, [FromBody] EntryDto dto) // TODO useless dto
    {
        var metric = _repo.GetMetric(metricName);
        var value = _repo.GetCutoff(metricName);
        
        var max = metric.Max;
        var withInRange = !max.HasValue || dto.Value <= max;
        var betterThanCutoff = metric.Direction == Direction.Increase ? value <= dto.Value : value >= dto.Value;

        return betterThanCutoff && withInRange;
    }
}