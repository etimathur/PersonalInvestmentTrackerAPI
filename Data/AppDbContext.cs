using Microsoft.EntityFrameworkCore;
using PersonalInvestmentTracker.Api.Models;

namespace PersonalInvestmentTracker.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Sip> Sips { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Sip>()
            .Property(s => s.MonthlyAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Sip>()
            .Property(s => s.ExpectedReturns)
            .HasPrecision(5, 2);
    }
}