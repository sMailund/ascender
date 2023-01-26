using ascender.Enum;

namespace ascender.DomainObjects;

public class Metric
{
    private string _name;
    private decimal _cutoff;
    public decimal? _max { get; }
    public Direction _direction { get; }

    public Metric(string name, decimal cutoff, decimal? max, Direction direction)
    {
        this._name = name;
        this._cutoff = cutoff;
        this._max = max;
        _direction = direction;
    }
}