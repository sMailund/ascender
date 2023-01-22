using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ascender.Dto;
using Newtonsoft.Json;

namespace acceptanceTests.driver;

public class HttpDriver
{

    private readonly HttpClient _client;

    public HttpDriver(HttpClient client)
    {
        _client = client;
    }

    public async Task CreateMetric(CreateMetricDto dto)
    {
        var myContent = JsonConvert.SerializeObject(dto);
        var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json"); 

        var response = await _client.PostAsync("/Metric", stringContent); 
        
        response.EnsureSuccessStatusCode(); 
    }

    public async Task CommitEntry(string metricName, int value)
    {
        var dto = new EntryDto
        {
            Name = metricName,
            Value = value
        };
        
        var myContent = JsonConvert.SerializeObject(dto);
        
        var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json"); 
        
        var response = await _client.PostAsync($"/Metric/{metricName}/commit", stringContent); 
        response.EnsureSuccessStatusCode(); 
    }

    public async Task<bool> ValidateEntry(string metricName, int value)
    {
        var dto = new EntryDto
        {
            Name = metricName,
            Value = value
        };
        
        var myContent = JsonConvert.SerializeObject(dto);
        
        var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json"); 
        
        var response = await _client.PostAsync($"/Metric/{metricName}/validate", stringContent); 
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return result.Equals("true");
    }
    
    public async Task AddDays(int days)
    {
        var response = await _client.PostAsync($"/Debug/addDays/{days}", new StringContent("")); 
        response.EnsureSuccessStatusCode(); 
    }
}