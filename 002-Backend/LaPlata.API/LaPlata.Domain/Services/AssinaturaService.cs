using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq.Expressions;
using LaPlata.Domain.Interfaces.Repositories.Cartoes;

namespace LaPlata.Domain.Services
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly IAssinaturaRepository _assinaturaRepository;
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IRepository<Log> _logErroRepository;
        private readonly IMapper _mapper;

        public AssinaturaService(IAssinaturaRepository assinaturaRepository, ICartaoRepository cartaoRepository, IRepository<Log> logErroRepository, IMapper mapper)
        {
            _assinaturaRepository = assinaturaRepository;
            _cartaoRepository = cartaoRepository;
            _logErroRepository = logErroRepository;
            _mapper = mapper;
        }

        public RespostaServico<ReadAssinaturaDTO> IncluirAssinatura(CreateAssinaturaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _mapper.Map<Assinatura>(DTO);
                var modelCartao = _cartaoRepository.Obter(x => x.Id == model.CartaoId).FirstOrDefault();
                if (modelCartao != null)
                {
                    if (_assinaturaRepository.Adicionar(model) == 0)
                        throw new Exception("Nenhum registro incluído no banco de dados.");
                    model.Cartao = modelCartao;
                    retorno.Valor = _mapper.Map<ReadAssinaturaDTO>(model);
                    retorno.Mensagem = "Registro incluído com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Erros = new List<string>() { "Cartão não encontrado" };
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

        public RespostaServico<List<ReadAssinaturaDTO>> ObterAssinaturas(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadAssinaturaDTO>>();
                busca = busca ?? string.Empty;
                var model = _assinaturaRepository.Obter(x => x.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadAssinaturaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<List<ReadAssinaturaDTO>> ObterAssinaturas(Expression<Func<Assinatura, bool>> predicate)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadAssinaturaDTO>>();
                var model = _assinaturaRepository.Obter(predicate).ToList();
                retorno.Valor = _mapper.Map<List<ReadAssinaturaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadAssinaturaDTO> ObterAssinatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _assinaturaRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadAssinaturaDTO>(model);
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

        public RespostaServico<ReadAssinaturaDTO> EditarAssinatura(int id, UpdateAssinaturaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _assinaturaRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_assinaturaRepository.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadAssinaturaDTO>(model);
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
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<ReadAssinaturaDTO> ExcluirAssinatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _assinaturaRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_assinaturaRepository.Excluir(model) == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados..");
                    retorno.Valor = _mapper.Map<ReadAssinaturaDTO>(model);
                    retorno.Mensagem = "Registro excluído com sucesso.";
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
    }
}
