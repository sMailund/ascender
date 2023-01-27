namespace ascender.DomainObjects;

public abstract class Metric
{
    public string Name { get; set; }
    public decimal Cutoff { get; set; }
    public decimal? Max { get; set; }
    public decimal? Minimum { get; set; }
    public decimal? Threshold { get; set; }
    public int Window { get; set; }

    public bool Validate(decimal newValue)
    {
        if (!WithinRange(newValue))
        {
            return false;
        }

        return IsWithinThreshold(newValue) || IsBetterThanCutoff(newValue);
    }

    protected abstract bool IsBetterThanCutoff(decimal newValue);
    protected abstract bool IsWithinThreshold(decimal newValue);

    private bool WithinRange(decimal newValue) =>
        (!Max.HasValue || newValue <= Max) && (!Minimum.HasValue || newValue >= Minimum);
}