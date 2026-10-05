using System.Text.Json.Serialization;

namespace PersonalInvestmentTracker.Api.Models;

public class MutualFundDetails
{
    [JsonPropertyName("scheme_code")]
    public int SchemeCode { get; set; }

    [JsonPropertyName("scheme_name")]
    public string? SchemeName { get; set; }

    [JsonPropertyName("fund_house")]
    public string? FundHouse { get; set; }

    [JsonPropertyName("plan_type")]
    public string? PlanType { get; set; }

    [JsonPropertyName("option_type")]
    public string? OptionType { get; set; }

    [JsonPropertyName("isin")]
    public string? Isin { get; set; }

    [JsonPropertyName("isin_reinvest")]
    public string? IsinReinvest { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("sub_category")]
    public string? SubCategory { get; set; }
}