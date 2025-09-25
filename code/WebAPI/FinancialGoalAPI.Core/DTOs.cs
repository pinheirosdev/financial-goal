namespace FinancialGoalAPI.Core;

// envio do usuário para a simulação
public record ParametrosSimulacao(
    decimal MetaValor,
    decimal AporteInicial,
    decimal AporteMensal,
    DateTime DataMeta
);

// retorno da simulação
public record ResultadoSimulacao(
    string TituloRecomendado,
    DateTime DataAlcancada,
    List<PontoProjecao> ProjecaoMensal
);

// ponto no gráfico de projeção
public record PontoProjecao(
    string MesAno,
    decimal ValorAcumulado
);