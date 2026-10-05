

using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalInvestmentTracker.Api.Data;
using PersonalInvestmentTracker.Api.Models;

[ApiController]
[Route("api/[controller]")]
public class SipsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly MutualFundService _mutualFundService;
    public SipsController(AppDbContext context, MutualFundService mutualFundService)
    {
        _context = context;
        _mutualFundService = mutualFundService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSips()
    {
        var sips = await _context.Sips.ToListAsync();
        return Ok(sips);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSip([FromBody] Sip sip)
    {
        _context.Sips.Add(sip);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSips), new { id = sip.Id }, sip);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSip(int id, [FromBody] Sip sip)
    {
        if(id != sip.Id)
        {
            return NotFound();
        }
        _context.Entry(sip).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Ok(sip);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSip(int id)
    {
        var sip = await _context.Sips.FindAsync(id);
        if (sip == null)
        {
            return NotFound();
        }

        _context.Sips.Remove(sip);
        await _context.SaveChangesAsync();
        return Ok(sip);
    }

    [HttpGet("currentPortfolioValue")]
    public async Task<IActionResult> GetCurrentPortfolioValue()
    {
        var sips = await _context.Sips.ToListAsync();
        decimal totalValue = 0;
        foreach (var sip in sips)
        {
            var navRates = await _mutualFundService.GetMutualFundHistoryAsync(
                sip.SchemeCode,
                sip.StartDate,
                DateTime.Now);
            if (navRates == null || !navRates.Data.Any())
            {
                continue;
            }
            decimal totalUnits = 0;
            var installmentDate = sip.StartDate;
            while (installmentDate <= DateTime.Now)
            {
                var nav = navRates.Data
                    .Where(x => x.NavDate <= installmentDate)
                    .OrderByDescending(x => x.NavDate)
                    .FirstOrDefault();
                if (nav != null)
                {
                    totalUnits += sip.MonthlyAmount / nav.Nav;
                }
                installmentDate = installmentDate.AddMonths(1);
            }
            var latestNav = navRates.Data
                .OrderByDescending(x => x.NavDate)
                .FirstOrDefault();
            if (latestNav != null)
            {
                totalValue += totalUnits * latestNav.Nav;
            }
        }
        return Ok(totalValue);
    }

    [HttpGet("fundPerformance")]
    public async Task<IActionResult> GetFundPerformance()
    {
        var sips = await _context.Sips.ToListAsync();
        var performanceData = new List<object>();

        foreach (var sip in sips)
        {
            var navRates = await _mutualFundService.GetMutualFundHistoryAsync(
                sip.SchemeCode,
                sip.StartDate,
                DateTime.Now);

            if (navRates == null || !navRates.Data.Any())
            {
                continue;
            }

            decimal totalUnits = 0;
            decimal totalInvested = 0;
            var installmentDate = sip.StartDate;

            while (installmentDate <= DateTime.Now)
            {
                var nav = navRates.Data
                    .Where(x => x.NavDate <= installmentDate)
                    .OrderByDescending(x => x.NavDate)
                    .FirstOrDefault();

                if (nav != null)
                {
                    totalUnits += sip.MonthlyAmount / nav.Nav;
                    totalInvested += sip.MonthlyAmount;
                }

                installmentDate = installmentDate.AddMonths(1);
            }

            var latestNav = navRates.Data
                .OrderByDescending(x => x.NavDate)
                .FirstOrDefault();

            if (latestNav != null)
            {
                decimal currentValue = totalUnits * latestNav.Nav;
                decimal returns = currentValue - totalInvested;

                performanceData.Add(new
                {
                    sip.FundName,
                    TotalInvested = totalInvested,
                    CurrentValue = currentValue,
                    Returns = returns
                });
            }
        }

        return Ok(performanceData);
    }

    [HttpGet("portfolioPerformance")]
    public async Task<IActionResult> GetPortfolioPerformance()
    {
        var sips = await _context.Sips.ToListAsync();

        if (!sips.Any())
        {
            return Ok(new List<object>());
        }

        var performanceData = new List<object>();

        var startDate = new DateTime(
            sips.Min(s => s.StartDate).Year,
            sips.Min(s => s.StartDate).Month,
            1);

        var endDate = new DateTime(
            DateTime.Now.Year,
            DateTime.Now.Month,
            1);

        for (var month = startDate; month <= endDate; month = month.AddMonths(1))
        {
            decimal portfolioValue = 0;

            foreach (var sip in sips)
            {
                if (sip.StartDate > month)
                {
                    continue;
                }

                var navRates = await _mutualFundService.GetMutualFundHistoryAsync(
                    sip.SchemeCode,
                    sip.StartDate,
                    month);

                if (navRates == null || !navRates.Data.Any())
                {
                    continue;
                }

                decimal totalUnits = 0;

                var installmentDate = sip.StartDate;

                while (installmentDate <= month)
                {
                    var nav = navRates.Data
                        .Where(x => x.NavDate <= installmentDate)
                        .OrderByDescending(x => x.NavDate)
                        .FirstOrDefault();

                    if (nav != null)
                    {
                        totalUnits += sip.MonthlyAmount / nav.Nav;
                    }

                    installmentDate = installmentDate.AddMonths(1);
                }

                var monthNav = navRates.Data
                    .Where(x => x.NavDate <= month)
                    .OrderByDescending(x => x.NavDate)
                    .FirstOrDefault();

                if (monthNav != null)
                {
                    portfolioValue += totalUnits * monthNav.Nav;
                }
            }

            performanceData.Add(new
            {
                Date = month.ToString("yyyy-MM"),
                Value = Math.Round(portfolioValue, 2)
            });
        }

        return Ok(performanceData);
    }
}