namespace FinancialGoalAPI.Core.Interfaces;

public interface ISimulacaoService
{
    Task<ResultadoSimulacao> Simular(ParametrosSimulacao parametros);
}