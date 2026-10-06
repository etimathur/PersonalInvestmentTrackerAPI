
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
            "PersonalInvestmentTracker/1.0"
        );
        request.Headers.Accept.ParseAdd("application/json");

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
            "PersonalInvestmentTracker/1.0"
        );
        request.Headers.Accept.ParseAdd("application/json");

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