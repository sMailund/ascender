using ascender.Dto;

namespace ascender.Repository;

class InMemoryMetricRepository : IMetricRepository
{
    private readonly Dictionary<string, List<MetricEntry>> _entries = new();
    private readonly Dictionary<string, CreateMetricDto> _metrics = new();

    public void CreateMetric(CreateMetricDto dto)
    {
        var entry = new MetricEntry
        {
            Value = 0,
            Time = DateTime.Now
        };
        _entries.Add(dto.Name, new List<MetricEntry> {entry});
        _metrics.Add(dto.Name, dto);
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
        _entries[name] = newList;;
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
}