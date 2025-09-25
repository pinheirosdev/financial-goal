using FinancialGoalAPI.Core;
using FinancialGoalAPI.Core.Interfaces;

namespace FinancialGoalAPI.Application.Services;

public class SimulacaoService : ISimulacaoService
{
    private readonly ITesouroRepository _tesouroRepository;

    public SimulacaoService(ITesouroRepository tesouroRepository)
    {
        _tesouroRepository = tesouroRepository;
    }

    public async Task<ResultadoSimulacao> Simular(ParametrosSimulacao parametros)
    {
        var tipoTituloRecomendado = RecomendarTipoTitulo(parametros.DataMeta);
        var taxaInfo = await _tesouroRepository.BuscarTaxaMaisRecentePorTipo(tipoTituloRecomendado);

        if (taxaInfo == null)
        {
            throw new Exception($"Não foi possível encontrar uma taxa para o tipo de título: {tipoTituloRecomendado}");
        }

        var projecaoMensal = CalcularProjecao(parametros, taxaInfo.Value.TaxaAnual);

        return new ResultadoSimulacao(
            taxaInfo.Value.NomeAmigavel,
            DateTime.Parse(projecaoMensal.Last().MesAno),
            projecaoMensal
        );
    }

    private string RecomendarTipoTitulo(DateTime dataMeta)
    {
        var anosParaMeta = (dataMeta - DateTime.Today).TotalDays / 365.25;
        if (anosParaMeta <= 2) return "SELIC";
        if (anosParaMeta > 2 && anosParaMeta <= 5) return "PREFIXADO";
        return "IPCA";
    }

    private List<PontoProjecao> CalcularProjecao(ParametrosSimulacao parametros, decimal taxaAnual)
    {
        var projecao = new List<PontoProjecao>();
        var saldoAtual = parametros.AporteInicial;
        var dataAtual = DateTime.Today;
        var taxaMensal = (double)(taxaAnual / 100) / 12;

        while (saldoAtual < parametros.MetaValor)
        {
            var jurosDoMes = saldoAtual * (decimal)taxaMensal;
            saldoAtual += jurosDoMes + parametros.AporteMensal;
            dataAtual = dataAtual.AddMonths(1);

            projecao.Add(new PontoProjecao(dataAtual.ToString("yyyy-MM-dd"), Math.Round(saldoAtual, 2)));

            if (projecao.Count > 1200) break; // limita a simulação em 100 anos
        }

        return projecao;
    }
}