using System.Net.Http;
using System.Threading.Tasks;

namespace acceptanceTests.drivers;

public class TimeDriver
{
    
    private readonly HttpClient _client;

    public TimeDriver(HttpClient client)
    {
        _client = client;
    }
    
    public async Task AddDays(int days)
    {
        var response = await _client.PostAsync($"/Debug/addDays/{days}", new StringContent("")); 
        response.EnsureSuccessStatusCode(); 
    }
}