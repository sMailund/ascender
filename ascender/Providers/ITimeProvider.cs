namespace ascender.Providers;

public interface ITimeProvider
{
    public DateTime Now();
    void AddDays(int days);
}