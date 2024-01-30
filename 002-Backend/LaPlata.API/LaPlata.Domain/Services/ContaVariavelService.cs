using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Interfaces.Repositories.Despesas;
using LaPlata.Domain.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class ContaVariavelService : IContaVariavelService
    {
        private readonly IContaVariavelRepository _contaVariavelRepository;
        private readonly IRepository<Log> _logErroRepository;
        private readonly IMapper _mapper;

        public ContaVariavelService(IContaVariavelRepository contaVariavelRepository, IRepository<PagamentoContaVariavel> contextPagamento, IRepository<Log> logErroRepository, IMapper mapper)
        {
            _contaVariavelRepository = contaVariavelRepository;
            _logErroRepository = logErroRepository;
            _mapper = mapper;
        }

        public RespostaServico<ReadContaVariavelDTO> IncluirContaVariavel(CreateContaVariavelDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadContaVariavelDTO>();
                var model = _mapper.Map<ContaVariavel>(DTO);
                if (_contaVariavelRepository.Adicionar(model) == 0)
                    throw new Exception("Nenhum registro incluído no banco de dados.");
                retorno.Valor = _mapper.Map<ReadContaVariavelDTO>(model);
                retorno.Mensagem = "Registro incluído com sucesso.";
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<List<ReadContaVariavelDTO>> ObterContasVariaveis(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadContaVariavelDTO>>();
                busca = busca ?? string.Empty;
                var model = _contaVariavelRepository.Obter(x => x.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadContaVariavelDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadContaVariavelDTO> ObterContaVariavel(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadContaVariavelDTO>();
                var model = _contaVariavelRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadContaVariavelDTO>(model);
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

        public RespostaServico<ReadContaVariavelDTO> EditarContaVariavel(int id, UpdateContaVariavelDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadContaVariavelDTO>();
                var model = _contaVariavelRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_contaVariavelRepository.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadContaVariavelDTO>(model);
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

        public RespostaServico<ReadContaVariavelDTO> ExcluirContaVariavel(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadContaVariavelDTO>();
                var model = _contaVariavelRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_contaVariavelRepository.Excluir(model) == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadContaVariavelDTO>(model);
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
