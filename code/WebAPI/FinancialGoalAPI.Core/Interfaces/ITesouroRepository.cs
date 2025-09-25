using FinancialGoalAPI.Core.Models;

namespace FinancialGoalAPI.Core.Interfaces;

public interface ITesouroRepository
{
    Task<(string NomeAmigavel, decimal TaxaAnual)?> BuscarTaxaMaisRecentePorTipo(string tipoTitulo);
}