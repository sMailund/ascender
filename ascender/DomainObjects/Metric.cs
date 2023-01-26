using ascender.Enum;

namespace ascender.DomainObjects;

public abstract class Metric
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
    
    public bool Validate(decimal newValue)
    {
        var withInRange = !Max.HasValue || newValue <= Max;
        
        var betterThanCutoff = Direction == Direction.Increase ? Cutoff <= newValue : Cutoff >= newValue;

        return betterThanCutoff && withInRange;
    }
}