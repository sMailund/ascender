using ascender.Dto;
using ascender.Providers;
using ascender.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ascender.controllers;

[ApiController]
[Route("[controller]")]
public class MetricController : ControllerBase
{
    private readonly IMetricRepository _repo;
    private readonly ITimeProvider _time;

    public MetricController(IMetricRepository repo, ITimeProvider time)
    {
        _repo = repo;
        _time = time;
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
        var value = _repo.GetCutoff(metricName);
        return value <= dto.Value;
    }
}