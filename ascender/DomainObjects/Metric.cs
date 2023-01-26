using ascender.Enum;

namespace ascender.DomainObjects;

public class Metric
{
    public string _name { get; }
    public  decimal _cutoff { get; set; }
    public decimal? _max { get; }
    public Direction _direction { get; }
    public int Window { get; }

    public Metric(string name, decimal? max, Direction direction, int window)
    {
        this._name = name;
        this._max = max;
        _direction = direction;
        Window = window;
    }
}