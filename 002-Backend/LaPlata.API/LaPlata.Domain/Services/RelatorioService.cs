using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IContext<Despesa> _contextDespesa;
        private readonly IContext<PagamentoConta> _contextConta;
        private readonly IContext<PagamentoContaVariavel> _contextContaVariavel;
        private readonly IContext<Fatura> _contextFatura;
        private readonly IMapper _mapper;

        public RelatorioService(IContext<Despesa> contextDespesa, IContext<PagamentoConta> contextConta, IContext<PagamentoContaVariavel> contextContaVariavel, IContext<Fatura> contextFatura, IMapper mapper)
        {
            _contextDespesa = contextDespesa;
            _contextConta = contextConta;
            _contextContaVariavel = contextContaVariavel;
            _contextFatura = contextFatura;
            _mapper = mapper;
        }

        public RespostaServico<List<DespesasMesDTO>> ObterDespesasMes(List<ObterDespesasMesDTO> DTO)
        {
            var retorno = new RespostaServico<List<DespesasMesDTO>>();

            try
            {
                var relatorio = new List<DespesasMesDTO>();

                foreach (var item in DTO)
                {
                    var despesas = _contextDespesa.Obter(x => x.DataDespesa.Month == item.Mes && x.DataDespesa.Year == item.Ano).ToList();
                    var pagamentosConta = _contextConta.Obter(x => x.DataPagamento.Month == item.Mes && x.DataPagamento.Year == item.Ano).ToList();
                    var pagamentosContaVariavel = _contextContaVariavel.Obter(x => x.DataPagamento.Month == item.Mes && x.DataPagamento.Year == item.Ano).ToList();
                    var faturas = _contextFatura.Obter(x => x.Mes == item.Mes && x.Ano == item.Ano).ToList();

                    var despesasMes = new DespesasMesDTO
                    {
                        Mes = item.Mes,
                        Ano = item.Ano,
                        TotalDespesas = despesas.Sum(x => x.ValorInteiro + (Convert.ToDecimal(x.ValorCentavos) / 100)),
                        TotalPagamentosConta = pagamentosConta.Sum(x => x.Conta.ValorInteiro + (Convert.ToDecimal(x.Conta.ValorCentavos) / 100)),
                        TotalPagamentosContaVariavel = pagamentosContaVariavel.Sum(x => x.ValorInteiro + (Convert.ToDecimal(x.ValorCentavos) / 100)),
                        TotalFaturas = faturas.Sum(x => Convert.ToDecimal(x.TotalFatura)),
                        Desesas = _mapper.Map<List<ReadDespesaDTO>>(despesas),
                        PagamntosConta = _mapper.Map<List<ReadPagamentoContaDTO>>(pagamentosConta),
                        PagamentosContaVariavel = _mapper.Map<List<ReadPagamentoContaVariavelDTO>>(pagamentosContaVariavel),
                        Faturas = _mapper.Map<List<ReadFaturaDTO>>(faturas)
                    };
                    despesasMes.TotalMes = despesasMes.TotalDespesas +
                                           despesasMes.TotalPagamentosConta +
                                           despesasMes.TotalPagamentosContaVariavel +
                                           despesasMes.TotalFaturas;

                    relatorio.Add(despesasMes);
                }

                retorno.Valor = relatorio;
                retorno.Status = EnumStatusResposta.SUCESSO;
                retorno.Mensagem = "Relatório gerado com sucesso!";
            }
            catch (Exception ex)
            {
                retorno.Status = EnumStatusResposta.ERRO;
                retorno.Mensagem = ex.Message;
            }

            return retorno;
        }
    }
}
