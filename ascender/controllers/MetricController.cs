using ascender.Dto;
using ascender.Factory;
using ascender.Services;
using Microsoft.AspNetCore.Mvc;

namespace ascender.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MetricController : ControllerBase
{
    private readonly MetricService _metricService;

    public MetricController(MetricService metricService)
    {
        _metricService = metricService;
    }

    [HttpPost]
    public string CreateMetric([FromBody] CreateMetricDto dto)
    {
        var metric = new MetricFactory()
            .Init(dto.Name, dto.Direction, dto.Window)
            .WithMax(dto.Max)
            .Build();
        
        _metricService.CreateNewMetric(metric);
        return dto.Name;
    }

    [HttpPost]
    [Route("{metricName}/commit")]
    public void Commit(string metricName, [FromBody] EntryDto dto) 
    {
        try
        {
            _metricService.Commit(metricName, dto.Value);
        }
        catch (ArgumentException)
        {
            BadRequest();
        }
    }
    
    [HttpPost]
    [Route("{metricName}/validate")]
    public bool Validate(string metricName, [FromBody] EntryDto dto) 
    {
        return _metricService.Validate(metricName, dto.Value);
    }
}