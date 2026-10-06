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
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 Chrome/140.0 Safari/537.36"
        );

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }
    
}