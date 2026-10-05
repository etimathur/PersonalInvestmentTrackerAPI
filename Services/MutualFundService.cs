
using System.Text.Json;
using PersonalInvestmentTracker.Api.Models;

public class MutualFundService
{
    private readonly HttpClient _httpClient;

    public MutualFundService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MutualFundsNav?> GetLatestNav(string schemeCode)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://mfnav.in/api/funds/{schemeCode}"
        );

        request.Headers.UserAgent.ParseAdd(
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 Chrome/140.0 Safari/537.36"
        );

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<MutualFundsNav>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result;
    }
    public async Task<MutualFundHistory?> GetMutualFundHistoryAsync(string schemeCode, DateTime startDate, DateTime endDate)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://mfnav.in/api/nav/{schemeCode}?start_date={startDate:yyyy-MM-dd}&end_date={endDate:yyyy-MM-dd}"
        );

        request.Headers.UserAgent.ParseAdd(
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 Chrome/140.0 Safari/537.36"
        );

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<MutualFundHistory>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return result;
    }
}