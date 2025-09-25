using Microsoft.EntityFrameworkCore;
using FinancialGoalAPI.Core.Models;

namespace FinancialGoalAPI.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Titulo> Titulos { get; set; }
    public DbSet<HistoricoTaxa> HistoricoTaxas { get; set; }
}