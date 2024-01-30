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
    public class PagamentoContaVariavelService : IPagamentoContaVariavelService
    {
        private readonly IPagamentoContaVariavelRepository _pagamentoContaVariavelRepository;
        private readonly IContaVariavelRepository _contaVariavelRepository;
        private readonly IRepository<Log> _logErroRepository;
        private readonly IMapper _mapper;

        public PagamentoContaVariavelService(IPagamentoContaVariavelRepository pagamentoContaVariavelRepository, IContaVariavelRepository contaVariavelRepository,IRepository<Log> logErroRepository, IMapper mapper)
        {
            _pagamentoContaVariavelRepository = pagamentoContaVariavelRepository;
            _contaVariavelRepository = contaVariavelRepository;
            _logErroRepository = logErroRepository;
            _mapper = mapper;
        }

        public RespostaServico<ReadPagamentoContaVariavelDTO> IncluirPagamentoContaVariavel(CreatePagamentoContaVariavelDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaVariavelDTO>();
                var model = _mapper.Map<PagamentoContaVariavel>(DTO);
                var modelConta = _contaVariavelRepository.Obter(x => x.Id == model.ContaVariavelId).FirstOrDefault();
                if (modelConta != null)
                {
                    if (_pagamentoContaVariavelRepository.Adicionar(model) == 0)
                        throw new Exception("Nenhum registro incluído no banco de dados.");
                    model.ContaVariavel = modelConta;
                    retorno.Valor = _mapper.Map<ReadPagamentoContaVariavelDTO>(model);
                    retorno.Mensagem = "Registro incluído com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Erros = new List<string>() { "Conta não encontrada" };
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

        public RespostaServico<List<ReadPagamentoContaVariavelDTO>> ObterPagamentosContaVariavel(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadPagamentoContaVariavelDTO>>();
                busca = busca ?? string.Empty;
                var model = _pagamentoContaVariavelRepository.Obter(x => x.ContaVariavel.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadPagamentoContaVariavelDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadPagamentoContaVariavelDTO> ObterPagamentoContaVariavel(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaVariavelDTO>();
                var model = _pagamentoContaVariavelRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadPagamentoContaVariavelDTO>(model);
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

        public RespostaServico<ReadPagamentoContaVariavelDTO> EditarPagamentoContaVariavel(int id, UpdatePagamentoContaVariavelDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaVariavelDTO>();
                var model = _pagamentoContaVariavelRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_pagamentoContaVariavelRepository.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadPagamentoContaVariavelDTO>(model);
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

        public RespostaServico<ReadPagamentoContaVariavelDTO> ExcluirPagamentoContaVariavel(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaVariavelDTO>();
                var model = _pagamentoContaVariavelRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_pagamentoContaVariavelRepository.Excluir(model) == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados..");
                    retorno.Valor = _mapper.Map<ReadPagamentoContaVariavelDTO>(model);
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
