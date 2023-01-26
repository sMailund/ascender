namespace ascender.DomainObjects;

public abstract class Metric
{
    public string Name { get; set;  }
    public decimal Cutoff { get; set; }
    public decimal? Max { get; set;  }
    public decimal? Minimum { get; set;  }
    public int Window { get; set;  }

    public bool Validate(decimal newValue)
    {
        var withInRange = (!Max.HasValue || newValue <= Max) && (!Minimum.HasValue || newValue >= Minimum);

        var betterThanCutoff = IsBetterThanCutoff(newValue);

        return betterThanCutoff && withInRange;
    }

    protected abstract bool IsBetterThanCutoff(decimal newValue);

}