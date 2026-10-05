namespace PersonalInvestmentTracker.Api.Models;

public class Sip
{
    public int Id { get; set; }

    public string SchemeCode { get; set; } = string.Empty;

    public string FundName { get; set; } = string.Empty;

    public decimal MonthlyAmount { get; set; }

    public DateTime StartDate { get; set; }

    public string Category { get; set; } = string.Empty;

    public decimal ExpectedReturns { get; set; }
}