using CenturyBelongingCalculator.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CenturyBelongingCalculator.Infrastructure;

public interface ICenturyBelongingCalculatorDbContext { }
public partial class CenturyBelongingCalculatorDbContext : DbContext, ICenturyBelongingCalculatorDbContext
{
    private readonly IConfiguration _configuration;
    public CenturyBelongingCalculatorDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Calc> Calcs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ApplicationDbContextConnection"));
    }
}
