namespace FinancialGoal.Core.Models;
public class Titulo
{
    public int Id { get; set; }
    public string NomeAmigavel { get; set; } = string.Empty;
    public string NomeOficial { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public DateTime DataVencimento { get; set; }
}