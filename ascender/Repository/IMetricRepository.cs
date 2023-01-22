namespace ascender.Repository;

public interface IMetricRepository
{
    public void CreateMetric(string name);
    public void MetricCommitted(string name, int value, DateTime time);
    public int GetCutoff(string name);
}