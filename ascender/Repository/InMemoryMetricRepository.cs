using ascender.Providers;

namespace ascender.Repository;

class InMemoryMetricRepository : IMetricRepository
{
    private readonly Dictionary<string, List<MetricEntry>> _metrics = new();
    private ITimeProvider _time;

    public InMemoryMetricRepository(ITimeProvider time)
    {
        _time = time;
    }

    public void CreateMetric(string name)
    {
        var entry = new MetricEntry
        {
            Value = 0,
            Time = _time.Now()
        };
        _metrics.Add(name, new List<MetricEntry> {entry});
    }

    public void MetricCommitted(string name, int value, DateTime time)
    {
        var entry = new MetricEntry
        {
            Value = value,
            Time = time
        };

        var metricEntries = _metrics[name];
        var newList = new List<MetricEntry>();
        newList.AddRange(metricEntries);
        newList.Add(entry);
        _metrics[name] = newList;;
    }

    public int GetCutoff(string name)
    {
        var metrics = _metrics[name];
        var lastSevenDays = metrics
            .Where(it => it.Time > _time.Now().AddDays(-7))
            .GroupBy(it => it.Time.Date, it => it.Value)
            .Select(it => it.Min())
            .ToList();

        return lastSevenDays.Count() == 0 ? metrics.Last().Value : lastSevenDays.Min();
    }
}