using ascender.Dto;
using ascender.Repository;
using ascender.Services;
using Microsoft.AspNetCore.Mvc;

namespace ascender.controllers;

[ApiController]
[Route("[controller]")]
public class MetricController : ControllerBase
{
    private readonly IMetricRepository _repo;
    private readonly MetricService _metricService;

    public MetricController(IMetricRepository repo, MetricService metricService)
    {
        _repo = repo;
        _metricService = metricService;
    }

    [HttpPost]
    public string Post([FromBody] CreateMetricDto dto)
    {
        _metricService.CreateNewMetric(dto);
        return dto.Name;
    }

    [HttpPost]
    [Route("{metricName}/commit")]
    public void Commit(string metricName, [FromBody] EntryDto dto) // TODO useless dto
    {
        try
        {
            _metricService.Commit(metricName, dto);
        }
        catch (ArgumentException)
        {
            BadRequest();
        }
    }
    
    [HttpPost]
    [Route("{metricName}/validate")]
    public bool Validate(string metricName, [FromBody] EntryDto dto) // TODO useless dto
    {
        return _metricService.Validate(metricName, dto);
    }
}