namespace FinancialGoal.Core.Models;
public class HistoricoTaxa
{
    public int Id { get; set; }
    public int TituloId { get; set; }
    public Titulo? Titulo { get; set; }
    public DateTime DataColeta { get; set; }
    public decimal TaxaRendimentoAnual { get; set; }
}