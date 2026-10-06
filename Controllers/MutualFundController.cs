using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/funds")]
public class MutualFundController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public MutualFundController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMutualFunds([FromQuery] string query)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://mfnav.in/api/funds/search?q={query}&page=1&page_size=2"
        );

        request.Headers.UserAgent.ParseAdd(
            "PersonalInvestmentTracker/1.0"
        );
        request.Headers.Accept.ParseAdd("application/json");

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }
    
}