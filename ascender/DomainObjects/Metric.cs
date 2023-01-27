namespace ascender.DomainObjects;

public abstract class Metric
{
    public string Name { get; set;  }
    public decimal Cutoff { get; set; }
    public decimal? Max { get; set;  }
    public decimal? Minimum { get; set;  }
    public decimal? Threshold { get; set; }
    public int Window { get; set;  }

    public bool Validate(decimal newValue)
    {
        var withInRange = WithinRange(newValue);
        if (!withInRange)
        {
            return false;
        }
        

        if (IsWithinThreshold(newValue))
        {
            return true;
        }

        var betterThanCutoff = IsBetterThanCutoff(newValue);

        return betterThanCutoff && withInRange;
    }

    protected abstract bool IsBetterThanCutoff(decimal newValue);
    protected abstract bool IsWithinThreshold(decimal newValue);

    private bool WithinRange(decimal newValue)
    {
        
        return (!Max.HasValue || newValue <= Max) && (!Minimum.HasValue || newValue >= Minimum);
    }

}