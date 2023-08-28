using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using LaPlata.Domain.Models.Comum;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly IContext<Fatura> _context;
        private readonly IContext<Compra> _contextCompra;
        private readonly IContext<Assinatura> _contextAssinatura;
        private readonly IContext<Cartao> _contextCartao;
        private readonly IContext<CompraFatura> _contextCompraFatura;
        private readonly IContext<AssinaturaFatura> _contextAssinaturaFatura;
        private readonly IContext<LogErro> _contextLogErro;
        private readonly IMapper _mapper;

        public FaturaService(
            IContext<Fatura> context, 
            IContext<Compra> contextCompra, 
            IContext<Assinatura> contextAssinatura, 
            IContext<Cartao> contextCartao, 
            IContext<CompraFatura> contextCompraFatura, 
            IContext<AssinaturaFatura> contextAssinaturaFatura,
            IContext<LogErro> contextLogErro,
            IMapper mapper)
        {
            _context = context;
            _contextCompra = contextCompra;
            _contextAssinatura = contextAssinatura;
            _contextCartao = contextCartao;
            _contextCompraFatura = contextCompraFatura;
            _contextAssinaturaFatura = contextAssinaturaFatura;
            _contextLogErro = contextLogErro;
            _mapper = mapper;
        }

        public RespostaServico<ReadFaturaDTO> IncluirFatura(CreateFaturaDTO DTO)
        {
            var retorno = new RespostaServico<ReadFaturaDTO>();
            var fatura = new Fatura();
            var comprasFatura = new ConcurrentBag<CompraFatura>();
            var assinaturasFatura = new ConcurrentBag<AssinaturaFatura>();

            try
            {
                fatura = _mapper.Map<Fatura>(DTO);

                #region Validações

                var resultadoValidacao = new ResultadoValidacao(true);
                // Validar cartão
                var cartao = _contextCartao.Obter(x => x.Id == fatura.CartaoId).FirstOrDefault();
                if (cartao == null)
                {
                    resultadoValidacao.Mensagens.Add("Cartão não encontrado.");
                    resultadoValidacao.Valido = false;
                }
                // Validar fatura existente
                var faturaExistente = _context.Obter(x => x.CartaoId == fatura.CartaoId && x.Mes == fatura.Mes && x.Ano == fatura.Ano).FirstOrDefault();
                if (faturaExistente != null)
                {
                    resultadoValidacao.Mensagens.Add("Já existe fatura para o cartão, mês e ano informados.");
                    resultadoValidacao.Valido = false;
                }

                #endregion

                if (resultadoValidacao.Valido)
                {
                    #region Incluir fatura

                    decimal totalCompras = 0;
                    decimal totalParcelas = 0;
                    decimal totalAssinaturas = 0;

                    try
                    {
                        fatura.Vencimento = new DateTime()
                            .AddYears(fatura.Ano - 1)
                            .AddMonths(fatura.Mes)
                            .AddDays(cartao.DiaVencimento == 31 ? cartao.DiaVencimento - 2 : cartao.DiaVencimento - 1);

                        if (_context.Adicionar(fatura) == 0)
                            throw new Exception("Não foi possível incluir a fatura.");
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao incluir fatura: " + e.Message, e);
                    }

                    #endregion

                    #region Incluir compras

                    try
                    {
                        // Obter compras da fatura
                        var compras = _contextCompra.Obter(x =>
                            // Compras feitas no mesmo ano da fatura e antes do dia do fechamento
                            ((x.DataCompra.Day <= cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano) && (x.DataCompra.Month + x.QtdParcelas - 1 >= fatura.Mes))
                            ||
                            // Compras feitas no mesmo ano da fatura e depois do dia do fechamento
                            ((x.DataCompra.Day > cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano) && (x.DataCompra.Month + x.QtdParcelas >= fatura.Mes))
                            ||
                            // Compras feitas no ano anterior a fatura e antes do dia do fechamento
                            ((x.DataCompra.Day <= cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano - 1) && ((13 - x.DataCompra.Month) + fatura.Mes <= x.QtdParcelas))
                            ||
                            // Compras feitas no ano anterior a fatura e depois do dia do fechamento
                            ((x.DataCompra.Day > cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano - 1) && ((12 - x.DataCompra.Month) + fatura.Mes <= x.QtdParcelas)))
                            .ToList();

                        foreach (var item in compras)
                        {
                            // Definir parcela
                            var parcela = 1;
                            if (item.DataCompra.Year == fatura.Ano)
                            {
                                if (item.DataCompra.Day <= cartao.DiaFechamento)
                                    parcela = fatura.Mes - item.DataCompra.Month + 1;
                                else
                                    parcela = fatura.Mes - item.DataCompra.Month;
                            }
                            else
                            {
                                if (item.DataCompra.Day <= cartao.DiaFechamento)
                                    parcela = (13 - item.DataCompra.Month) + fatura.Mes;
                                else
                                    parcela = (12 - item.DataCompra.Month) + fatura.Mes;
                            }

                            // Incluir compra na fatura
                            var compraFatura = new CompraFatura()
                            {
                                FaturaId = fatura.Id,
                                CompraId = item.Id,
                                Parcela = parcela,
                                ValorParcela = (item.ValorInteiro + (Convert.ToDecimal(item.ValorCentavos) / 100)) / item.QtdParcelas
                            };
                            if (_contextCompraFatura.Adicionar(compraFatura) == 0)
                                throw new Exception("Não foi possível incluir uma compra.");
                            comprasFatura.Add(compraFatura);

                            if (item.QtdParcelas > 1)
                                totalParcelas += compraFatura.ValorParcela;
                            else
                                totalCompras += compraFatura.ValorParcela;
                        }

                        fatura.ComprasFatura = comprasFatura.ToList();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao incluir compras na fatura: " + e.Message, e);
                    }

                    #endregion

                    #region Incluir assinaturas

                    // Obter assinaturas e incluir na fatura
                    try
                    {
                        var assinaturas = _contextAssinatura.Obter(x => x.CartaoId == fatura.CartaoId).ToList();

                        foreach (var item in assinaturas)
                        {
                            // Incluir assinaturas na fatura
                            var assinaturaFatura = new AssinaturaFatura()
                            {
                                FaturaId = fatura.Id,
                                AssinaturaId = item.Id
                            };

                            if (_contextAssinaturaFatura.Adicionar(assinaturaFatura) == 0)
                                throw new Exception("Não foi possível incluir uma assinatura.");
                            assinaturasFatura.Add(assinaturaFatura);

                            totalAssinaturas += item.ValorInteiro + (Convert.ToDecimal(item.ValorCentavos) / 100);
                        }

                        fatura.AssinaturasFatura = assinaturasFatura.ToList();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao incluir assinaturas na fatura: " + e.Message, e);
                    }

                    #endregion

                    #region Atualizar totais

                    try
                    {
                        fatura.TotalAssinaturas = totalAssinaturas;
                        fatura.TotalCompras = totalCompras;
                        fatura.TotalComprasParceladas = totalParcelas;
                        fatura.TotalFatura = totalAssinaturas + totalCompras + totalParcelas;
                        _context.SalvarAlteracoes();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao atualizar valores da fatura: " + e.Message, e);
                    }

                    #endregion

                    retorno.Valor = _mapper.Map<ReadFaturaDTO>(fatura);
                    retorno.Mensagem = "Fatura incluída com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO; 
                }
                else
                {
                    retorno.Mensagem = "Validação rejeitada.";
                    retorno.Erros = resultadoValidacao.Mensagens;
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                foreach (var item in comprasFatura)
                {
                    _contextCompraFatura.Excluir(item, true);
                }
                foreach (var item in assinaturasFatura)
                {
                    _contextAssinaturaFatura.Excluir(item, true);
                }
                if (fatura.Id > 0)
                    _context.Excluir(fatura, true);
                throw;
            }
        }

        public RespostaServico<List<ReadFaturaDTO>> ObterFaturas(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadFaturaDTO>>();
                busca = busca ?? string.Empty;
                var model = _context.Obter(x => x.Cartao.Nome.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadFaturaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadFaturaDTO> ObterFatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadFaturaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadFaturaDTO>(model);
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Mensagem = "Registro não encontrado.";
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadFaturaDTO> ExcluirFatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadFaturaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (model.ComprasFatura != null)
                    {
                        Parallel.ForEach(model.ComprasFatura, item =>
                        {
                            if (_contextCompraFatura.Excluir(item) == 0)
                                throw new Exception("Erro ao exlcuir uma compra da fatura.");
                        });
                    }
                    if (model.AssinaturasFatura != null)
                    {
                        Parallel.ForEach(model.AssinaturasFatura, item =>
                        {
                            if (_contextAssinaturaFatura.Excluir(item) == 0)
                                throw new Exception("Erro ao exlcuir uma assinatura da fatura.");
                        });
                    }

                    if (_context.Excluir(model) == 0)
                        throw new Exception("Não foi possível excluir a fatura.");

                    retorno.Valor = _mapper.Map<ReadFaturaDTO>(model);
                    retorno.Mensagem = "Fatura excluída com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Mensagem = "Registro não encontrado.";
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }
                
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }
    }
}
