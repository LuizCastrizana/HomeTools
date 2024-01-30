using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class CartaoService : ICartaoService
    {
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IRepository<Log> _LogErroRepository;
        private readonly IMapper _mapper;

        public CartaoService(ICartaoRepository cartaoRepository, IRepository<Log> logErroRepository, IMapper mapper)
        {
            _cartaoRepository = cartaoRepository;
            _LogErroRepository = logErroRepository;
            _mapper = mapper;
        }

        public RespostaServico<ReadCartaoDTO> IncluirCartao(CreateCartaoDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadCartaoDTO>();
                var model = _mapper.Map<Cartao>(DTO);
                if (_cartaoRepository.Adicionar(model) == 0)
                    throw new Exception("Nenhum registro incluído no banco de dados.");
                retorno.Valor = _mapper.Map<ReadCartaoDTO>(model);
                retorno.Mensagem = "Registro incluído com sucesso.";
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _LogErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<List<ReadCartaoDTO>> ObterCartoes(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadCartaoDTO>>();
                busca = busca ?? string.Empty;
                var model = _cartaoRepository.Obter(x => x.Nome.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadCartaoDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _LogErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadCartaoDTO> ObterCartao(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadCartaoDTO>();
                var model = _cartaoRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadCartaoDTO>(model);
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
                _LogErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadCartaoDTO> EditarCartao(int id, UpdateCartaoDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadCartaoDTO>();
                var model = _cartaoRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_cartaoRepository.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadCartaoDTO>(model);
                    retorno.Mensagem = "Registro atualizado com sucesso.";
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
                _LogErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<int> ExcluirCartao(int id)
        {            
            try
            {
                var retorno = new RespostaServico<int>();
                var model = _cartaoRepository.Obter(x => x.Id == id).FirstOrDefault();
                
                #region Validações

                var exclusaoValida = true;
                var mensagensValidacao = new List<string>();
                if (model == null)
                {
                    mensagensValidacao.Add("Cartão não encontrado");
                    exclusaoValida = false;
                }
                else
                {
                    if (model.Compras != null && model.Compras.Count > 0)
                    {
                        mensagensValidacao.Add("Não é possível excluir um cartão com compras cadastradas");
                        exclusaoValida = false;
                    }
                    if (model.Assinaturas != null && model.Assinaturas.Count > 0)
                    {
                        mensagensValidacao.Add("Não é possível excluir um cartão com assinaturas cadastradas");
                        exclusaoValida = false;
                    }
                    if (model.Faturas != null && model.Faturas.Count > 0)
                    {
                        mensagensValidacao.Add("Não é possível excluir um cartão com faturas cadastradas");
                        exclusaoValida = false;
                    }
                }

                #endregion

                if (exclusaoValida)
                {
                    model ??= new Cartao();
                    if (_cartaoRepository.Excluir(model) == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Mensagem = "Registro excluído com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Mensagem = "Validação rejeitada";
                    retorno.Erros = mensagensValidacao;
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }
                return retorno;
            }
            catch (Exception ex)
            {
                _LogErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }
    }
}
