using ascender.Dto;
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
        _metricService.CreateNewMetric(dto);
        return dto.Name;
    }

    [HttpPost]
    [Route("{metricName}/commit")]
    public void Commit(string metricName, [FromBody] EntryDto dto) 
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
    public bool Validate(string metricName, [FromBody] EntryDto dto) 
    {
        return _metricService.Validate(metricName, dto.Value);
    }
}