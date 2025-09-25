using Microsoft.EntityFrameworkCore;
using FinancialGoalAPI.Core.Interfaces;

namespace FinancialGoalAPI.Infrastructure.Data;

public class TesouroRepository : ITesouroRepository
{
    private readonly AppDbContext _context;

    public TesouroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(string NomeAmigavel, decimal TaxaAnual)?> BuscarTaxaMaisRecentePorTipo(string tipoTitulo)
    {
        var resultado = await _context.HistoricoTaxas
            .AsNoTracking()
            .Include(ht => ht.Titulo)
            .Where(ht => ht.Titulo != null && ht.Titulo.Tipo == tipoTitulo)
            .OrderByDescending(ht => ht.DataColeta)
            .Select(ht => new { ht.Titulo!.NomeAmigavel, ht.TaxaRendimentoAnual })
            .FirstOrDefaultAsync();

        if (resultado == null)
        {
            return null;
        }

        return (resultado.NomeAmigavel, resultado.TaxaRendimentoAnual);
    }
}