namespace ascender.Providers;

class FakeTimeProvider : ITimeProvider
{
    private DateTime _now = new DateTime(2023, 1, 1, 10, 20, 30);
    public DateTime Now()
    {
        return _now;
    }

    public void AddDays(int days)
    {
        _now = _now.AddDays(days);
    }
}