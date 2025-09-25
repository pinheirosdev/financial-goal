namespace FinancialGoalAPI.Core;

// envio do usu�rio para a simula��o
public record ParametrosSimulacao(
    decimal MetaValor,
    decimal AporteInicial,
    decimal AporteMensal,
    DateTime DataMeta
);

// retorno da simula��o
public record ResultadoSimulacao(
    string TituloRecomendado,
    DateTime DataAlcancada,
    List<PontoProjecao> ProjecaoMensal
);

// ponto no gr�fico de proje��o
public record PontoProjecao(
    string MesAno,
    decimal ValorAcumulado
);