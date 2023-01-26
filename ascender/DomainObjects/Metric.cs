using ascender.Enum;

namespace ascender.DomainObjects;

public class Metric
{
    public string Name { get; }
    public  decimal Cutoff { get; set; }
    public decimal? Max { get; }
    public Direction Direction { get; }
    public int Window { get; }

    public Metric(string name, decimal? max, Direction direction, int window)
    {
        Name = name;
        Max = max;
        Direction = direction;
        Window = window;
    }
}