using System.Text.Json.Serialization;

namespace PersonalInvestmentTracker.Api.Models;

public class MutualFundNavHistory
{
    [JsonPropertyName("nav_date")]
    public DateTime NavDate { get; set; }

    [JsonPropertyName("nav")]
    public decimal Nav { get; set; }

    [JsonPropertyName("day_change_pct")]
    public decimal DayChangePct { get; set; }
}