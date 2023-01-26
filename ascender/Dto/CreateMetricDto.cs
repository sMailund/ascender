using ascender.Enum;

namespace ascender.Dto;

public class CreateMetricDto
{
    public string Name { get; init; }
    public Direction Direction { get; init; }
    public int Window { get; init; }
    public decimal? Max { get; init; }
    public decimal? Minimum { get; init; }
}