namespace ascender.DomainObjects;

public abstract class Metric
{
    public string Name { get; set;  }
    public decimal Cutoff { get; set; }
    public decimal? Max { get; set;  }
    public decimal? Minumum { get; set;  }
    public int Window { get; set;  }

    public bool Validate(decimal newValue)
    {
        var withInRange = (!Max.HasValue || newValue <= Max) && (!Minumum.HasValue || newValue >= Minumum);

        var betterThanCutoff = IsBetterThanCutoff(newValue);

        return betterThanCutoff && withInRange;
    }

    protected abstract bool IsBetterThanCutoff(decimal newValue);

}