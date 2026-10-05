namespace PersonalInvestmentTracker.Api.Models;
using System.Text.Json.Serialization;

public class MutualFundsNav
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

    [JsonPropertyName("fund_house_slug")]
    public string? FundHouseSlug { get; set; }

    [JsonPropertyName("latest_nav_date")]
    public DateTime? LatestNavDate { get; set; }

    [JsonPropertyName("latest_nav")]
    public decimal LatestNav { get; set; }

    [JsonPropertyName("latest_day_change_pct")]
    public decimal LatestDayChangePct { get; set; }

    [JsonPropertyName("earliest_nav_date")]
    public DateTime? EarliestNavDate { get; set; }

    [JsonPropertyName("scheme_launch_date")]
    public DateTime? SchemeLaunchDate { get; set; }

    [JsonPropertyName("scheme_nfo_closed_date")]
    public DateTime? SchemeNfoClosedDate { get; set; }

    [JsonPropertyName("fund_structure")]
    public string? FundStructure { get; set; }

    [JsonPropertyName("daily_metrics")]
    public object? DailyMetrics { get; set; }

    [JsonPropertyName("last_updated")]
    public string? LastUpdated { get; set; }

    [JsonPropertyName("source")]
    public string? Source { get; set; }
}