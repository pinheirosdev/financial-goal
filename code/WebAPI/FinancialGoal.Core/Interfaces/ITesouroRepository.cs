using FinancialGoal.Core.Models;

namespace FinancialGoal.Core.Interfaces;

public interface ITesouroRepository
{
    Task<(string NomeAmigavel, decimal TaxaAnual)?> BuscarTaxaMaisRecentePorTipo(string tipoTitulo);
}