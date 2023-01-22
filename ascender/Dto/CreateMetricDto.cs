using ascender.Enum;

namespace ascender.Dto;

public class CreateMetricDto
{
    public string Name { get; init; }
    public MetricType Type { get; init; }
    public Direction Direction { get; init; }
}