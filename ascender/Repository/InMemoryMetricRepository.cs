using ascender.DomainObjects;
using ascender.Dto;

namespace ascender.Repository;

class InMemoryMetricRepository : IMetricRepository
{
    private readonly Dictionary<string, List<MetricEntry>> _entries = new();
    private readonly Dictionary<string, Metric> _metrics = new();

    public void CreateMetric(CreateMetricDto dto)
    {
    }

    public void CreateMetric(Metric metric)
    {
        var entry = new MetricEntry
        {
            Value = 0,
            Time = DateTime.Now
        };
        _entries.Add(metric.Name, new List<MetricEntry> {entry});
        _metrics.Add(metric.Name, metric);
    }

    public void MetricCommitted(string name, decimal value)
    {
        var entry = new MetricEntry
        {
            Value = value,
            Time = DateTime.Now
        };

        var metricEntries = _entries[name];
        var newList = new List<MetricEntry>();
        newList.AddRange(metricEntries);
        newList.Add(entry);
        _entries[name] = newList;
        _metrics[name].Cutoff = GetCutoff(name);
    }

    public decimal GetCutoff(string name)
    {
        var metric = _metrics[name];
        var metrics = _entries[name];
        return metrics
            .TakeLast(metric.Window)
            .Select(it => it.Value)
            .Min();
    }

    public Metric GetMetric(string name) => _metrics[name];
}