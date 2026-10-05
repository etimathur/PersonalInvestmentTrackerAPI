using System.Text.Json.Serialization;

namespace PersonalInvestmentTracker.Api.Models;

public class MutualFundHistory
{
    [JsonPropertyName("fund_details")]
    public MutualFundDetails? FundDetails { get; set; }

    [JsonPropertyName("data")]
    public List<MutualFundNavHistory> Data { get; set; } = [];

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("page_size")]
    public int PageSize { get; set; }
}