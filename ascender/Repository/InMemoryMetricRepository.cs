using ascender.Dto;
using ascender.Providers;

namespace ascender.Repository;

class InMemoryMetricRepository : IMetricRepository
{
    private readonly Dictionary<string, List<MetricEntry>> _entries = new();
    private readonly Dictionary<string, CreateMetricDto> _metrics = new();
    private ITimeProvider _time;

    public InMemoryMetricRepository(ITimeProvider time)
    {
        _time = time;
    }

    public void CreateMetric(CreateMetricDto dto)
    {
        var entry = new MetricEntry
        {
            Value = 0,
            Time = _time.Now()
        };
        _entries.Add(dto.Name, new List<MetricEntry> {entry});
        _metrics.Add(dto.Name, dto);
    }

    public void MetricCommitted(string name, int value, DateTime time)
    {
        var entry = new MetricEntry
        {
            Value = value,
            Time = time
        };

        var metricEntries = _entries[name];
        var newList = new List<MetricEntry>();
        newList.AddRange(metricEntries);
        newList.Add(entry);
        _entries[name] = newList;;
    }

    public int GetCutoff(string name)
    {
        var metric = _metrics[name];
        var metrics = _entries[name];
        return metrics
            .TakeLast(metric.EvaluationWindow)
            .Select(it => it.Value)
            .Min();
    }
}