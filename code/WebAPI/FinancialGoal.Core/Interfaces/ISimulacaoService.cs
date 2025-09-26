namespace FinancialGoal.Core.Interfaces;

public interface ISimulacaoService
{
    Task<ResultadoSimulacao> Simular(ParametrosSimulacao parametros);
}