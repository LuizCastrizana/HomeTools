using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Models;
using LaPlata.Domain.Models.Comum;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class FaturaService : IFaturaService
    {
        private readonly IFaturaRepository _faturaRepository;
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IRepository<CompraFatura> _compraFaturaRepository;
        private readonly IRepository<AssinaturaFatura> _assinaturaFaturaRepository;
        private readonly IRepository<Log> _logErroRepository;
        private readonly IMapper _mapper;

        public FaturaService(
            IFaturaRepository faturaRepository, 
            ICartaoRepository cartaoRepository, 
            IRepository<CompraFatura> compraFaturaRepository, 
            IRepository<AssinaturaFatura> assinaturaFaturaRepository,
            IRepository<Log> logErroRepository,
            IMapper mapper)
        {
            _faturaRepository = faturaRepository;
            _cartaoRepository = cartaoRepository;
            _compraFaturaRepository = compraFaturaRepository;
            _assinaturaFaturaRepository = assinaturaFaturaRepository;
            _logErroRepository = logErroRepository;
            _mapper = mapper;
        }

        public RespostaServico<ReadFaturaDTO> IncluirOuAtualizarFatura(CreateFaturaDTO DTO)
        {
            var retorno = new RespostaServico<ReadFaturaDTO>();
            var fatura = new Fatura();
            var resultadoValidacao = new ResultadoValidacao(true);
            var comprasIncluidas = new List<CompraFatura>();
            var assinaturasIncluidas = new List<AssinaturaFatura>();
            Cartao? cartao;

            try
            {
                fatura = _mapper.Map<Fatura>(DTO);
                cartao = _cartaoRepository.Obter(x => x.Id == fatura.CartaoId).FirstOrDefault();

                #region Validações

                // Validar cartão
                if (cartao == null)
                {
                    resultadoValidacao.Mensagens.Add("Cartão não encontrado");
                    resultadoValidacao.Valido = false;
                }
                // Validar mês
                if (fatura.Mes < 1 || fatura.Mes > 12)
                {
                    resultadoValidacao.Mensagens.Add("Mês inválido");
                    resultadoValidacao.Valido = false;
                }
                // Validar ano
                if (fatura.Ano < 1920 || fatura.Ano > (DateTime.Now.Year + 5))
                {
                    resultadoValidacao.Mensagens.Add("Ano inválido");
                    resultadoValidacao.Valido = false;
                }

                #endregion

                if (resultadoValidacao.Valido)
                {
                    Fatura? faturaExistente = null;

                    if (cartao.Faturas != null && cartao.Faturas.Count > 0)
                        faturaExistente = cartao.Faturas.Where(x => x.Mes == fatura.Mes && x.Ano == fatura.Ano).FirstOrDefault();

                    if (faturaExistente != null)
                        fatura = faturaExistente;
                    else
                    {
                        #region Incluir fatura

                        try
                        {
                            fatura.Vencimento = new DateTime()
                                .AddYears(fatura.Ano - 1)
                                .AddMonths(fatura.Mes)
                                .AddDays(cartao.DiaVencimento == 31 ? cartao.DiaVencimento - 2 : cartao.DiaVencimento - 1);

                            if (_faturaRepository.Adicionar(fatura) == 0)
                                throw new Exception("Nenhum registro incluído no banco de dados.");

                            fatura.Cartao = cartao;
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Erro ao incluir fatura: " + e.Message, e);
                        }

                        #endregion
                    }
                    
                    fatura.AssinaturasFatura ??= new List<AssinaturaFatura>();
                    fatura.ComprasFatura ??= new List<CompraFatura>();

                    #region Incluir compras

                    try
                    {
                        var comprasIncluir = ObterComprasAIncluir(fatura, cartao);

                        foreach (var item in comprasIncluir)
                        {
                            var parcela = DefinirParcela(item, fatura);

                            // Incluir compra na fatura
                            var compraFatura = new CompraFatura()
                            {
                                FaturaId = fatura.Id,
                                CompraId = item.Id,
                                Parcela = parcela,
                                ValorParcela = (item.ValorInteiro + (Convert.ToDecimal(item.ValorCentavos) / 100)) / item.QtdParcelas
                            };
                            if (_compraFaturaRepository.Adicionar(compraFatura) == 0)
                                throw new Exception("Não foi possível incluir uma compra.");

                            if (item.QtdParcelas > 1)
                                fatura.TotalComprasParceladas += compraFatura.ValorParcela;
                            else
                                fatura.TotalCompras += compraFatura.ValorParcela;

                            comprasIncluidas.Add(compraFatura);
                            fatura.ComprasFatura.Add(compraFatura);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao incluir compras na fatura: " + e.Message, e);
                    }

                    #endregion

                    #region Incluir assinaturas

                    try
                    {
                        var assinaturasIncluir = cartao.Assinaturas.Where(x => x.CartaoId == fatura.CartaoId && x.AssinaturasFatura.All(x => x.FaturaId != fatura.Id)).ToList();

                        foreach (var item in assinaturasIncluir)
                        {
                            var assinaturaFatura = new AssinaturaFatura()
                            {
                                FaturaId = fatura.Id,
                                AssinaturaId = item.Id
                            };

                            if (_assinaturaFaturaRepository.Adicionar(assinaturaFatura) == 0)
                                throw new Exception("Não foi possível incluir uma assinatura.");

                            fatura.TotalAssinaturas += item.ValorInteiro + (Convert.ToDecimal(item.ValorCentavos) / 100);

                            assinaturasIncluidas.Add(assinaturaFatura);
                            fatura.AssinaturasFatura.Add(assinaturaFatura);
                        }
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao incluir assinaturas na fatura: " + e.Message, e);
                    }

                    #endregion

                    #region Atualizar total

                    try
                    {
                        fatura.TotalFatura = fatura.TotalAssinaturas + fatura.TotalCompras + fatura.TotalComprasParceladas;

                        _faturaRepository.SalvarAlteracoes();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Erro ao atualizar valores da fatura: " + e.Message, e);
                    }

                    #endregion

                    retorno.Valor = _mapper.Map<ReadFaturaDTO>(fatura);
                    retorno.Mensagem = "Fatura incluída ou atualizada com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO; 
                }
                else
                {
                    retorno.Mensagem = "Validação rejeitada";
                    retorno.Erros = resultadoValidacao.Mensagens;
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }
        
        public RespostaServico<List<ReadFaturaDTO>> ObterFaturas(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadFaturaDTO>>();
                busca = busca ?? string.Empty;
                var model = _faturaRepository.Obter(x => x.Cartao.Nome.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadFaturaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadFaturaDTO> ObterFatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadFaturaDTO>();
                var model = _faturaRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadFaturaDTO>(model);
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Erros = new List<string>() { "Registro não encontrado" };
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadFaturaDTO> ExcluirFatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadFaturaDTO>();
                var model = _faturaRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (model.ComprasFatura != null)
                    {
                        Parallel.ForEach(model.ComprasFatura, item =>
                        {
                            if (_compraFaturaRepository.Excluir(item) == 0)
                                throw new Exception("Erro ao exlcuir uma compra da fatura.");
                        });
                    }
                    if (model.AssinaturasFatura != null)
                    {
                        Parallel.ForEach(model.AssinaturasFatura, item =>
                        {
                            if (_assinaturaFaturaRepository.Excluir(item) == 0)
                                throw new Exception("Erro ao exlcuir uma assinatura da fatura.");
                        });
                    }

                    if (_faturaRepository.Excluir(model) == 0)
                        throw new Exception("Não foi possível excluir a fatura.");

                    retorno.Valor = _mapper.Map<ReadFaturaDTO>(model);
                    retorno.Mensagem = "Fatura excluída com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Erros = new List<string>() { "Registro não encontrado" };
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }
                
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadFaturaDTO> RealizarPagamentoFatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadFaturaDTO>();

                var fatura = _faturaRepository.Obter(x => x.Id == id).FirstOrDefault();

                if (fatura != null)
                {
                    fatura.Paga = true;

                    var compras = new ConcurrentBag<CompraFatura>(fatura.ComprasFatura);

                    Parallel.ForEach(compras, item =>
                    {
                        item.Compra.Paga = true;
                    });

                    if (_faturaRepository.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");

                    retorno.Valor = _mapper.Map<ReadFaturaDTO>(fatura);
                    retorno.Mensagem = "Fatura paga com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Erros = new List<string>() { "Registro não encontrado" };
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<string> GerarFaturas()
        {
            var retorno = new RespostaServico<string>();
            var contadorFaturas = 0;
            try 
            {
                var cartoes = _cartaoRepository.Obter(x => x.Ativo && x.DiaFechamento == DateTime.Now.Day).ToList();
                foreach (var cartao in cartoes)
                {
                    var faturaAtual = _faturaRepository.Obter(x => x.CartaoId == cartao.Id && x.Mes == DateTime.Now.Month && x.Ano == DateTime.Now.Year).ToList();
                    if (faturaAtual.Count == 0)
                    {
                        var fatura = new CreateFaturaDTO()
                        {
                            CartaoId = cartao.Id,
                            Mes = DateTime.Now.Month,
                            Ano = DateTime.Now.Year
                        };
                        IncluirOuAtualizarFatura(fatura);
                        contadorFaturas++;
                    }
                }
                retorno.Status = EnumStatusResposta.SUCESSO;
                retorno.Mensagem = "Faturas geradas com sucesso.";
                retorno.Valor = "Qtd de faturas geradas: " + contadorFaturas.ToString();
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
            return retorno;
        }

        #region Métodos privados

        private List<Compra> ObterComprasAIncluir(Fatura fatura, Cartao cartao)
        {
            var compras = new List<Compra>();

            // Obter compras da fatura
            var comprasFatura = fatura.ComprasFatura.Where(x => x.FaturaId == fatura.Id).ToList();

            // Obter compras do cartão
            var comprasCartao = cartao.Compras.Where(x =>
                                // Compras feitas no mesmo ano da fatura e antes do dia do fechamento
                                ((x.DataCompra.Day < fatura.Cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano) && (x.DataCompra.Month + x.QtdParcelas - 1 >= fatura.Mes))
                                ||
                                // Compras feitas no mesmo ano da fatura e depois do dia do fechamento
                                ((x.DataCompra.Day >= fatura.Cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano) && (x.DataCompra.Month + x.QtdParcelas >= fatura.Mes))
                                ||
                                // Compras feitas no ano anterior a fatura e antes do dia do fechamento
                                ((x.DataCompra.Day < fatura.Cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano - 1) && ((13 - x.DataCompra.Month) + fatura.Mes <= x.QtdParcelas))
                                ||
                                // Compras feitas no ano anterior a fatura e depois do dia do fechamento
                                ((x.DataCompra.Day >= fatura.Cartao.DiaFechamento) && (x.DataCompra.Year == fatura.Ano - 1) && ((12 - x.DataCompra.Month) + fatura.Mes <= x.QtdParcelas)))
                                .ToList();

            // Obter compras que não estão na fatura
            compras = comprasCartao.Where(x => !comprasFatura.Any(y => y.CompraId == x.Id)).ToList();

            return compras;
        }

        private int DefinirParcela(Compra compra, Fatura fatura)
        {
            var parcela = 1;
            if (compra.DataCompra.Year == fatura.Ano)
            {
                if (compra.DataCompra.Day <= fatura.Cartao.DiaFechamento)
                    parcela = fatura.Mes - compra.DataCompra.Month + 1;
                else
                    parcela = fatura.Mes - compra.DataCompra.Month;
            }
            else
            {
                if (compra.DataCompra.Day <= fatura.Cartao.DiaFechamento)
                    parcela = (13 - compra.DataCompra.Month) + fatura.Mes;
                else
                    parcela = (12 - compra.DataCompra.Month) + fatura.Mes;
            }
            return parcela;
        }

        #endregion
    }
}
