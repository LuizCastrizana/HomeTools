using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class PagamentoContaService : IPagamentoContaService
    {
        private readonly IContext<PagamentoConta> _context;
        private readonly IContext<Conta> _contextConta;
        private readonly IContext<LogErro> _contextLogErro;
        private readonly IMapper _mapper;

        public PagamentoContaService(IContext<PagamentoConta> context, IContext<Conta> contextConta, IContext<LogErro> contextLogErro, IMapper mapper)
        {
            _context = context;
            _contextConta = contextConta;
            _contextLogErro = contextLogErro;
            _mapper = mapper;
        }

        public RespostaServico<ReadPagamentoContaDTO> IncluirPagamentoConta(CreatePagamentoContaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaDTO>();
                var model = _mapper.Map<PagamentoConta>(DTO);
                var modelConta = _contextConta.Obter(x => x.Id == model.ContaId).FirstOrDefault();
                if (modelConta != null)
                {
                    if (_context.Adicionar(model) == 0)
                        throw new Exception("Nenhum registro incluído no banco de dados.");
                    model.Conta = modelConta;
                    retorno.Valor = _mapper.Map<ReadPagamentoContaDTO>(model);
                    retorno.Mensagem = "Registro incluído com sucesso.";
                    retorno.Status = EnumStatusResposta.SUCESSO;
                }
                else
                {
                    retorno.Mensagem = "Conta não encontrada.";
                    retorno.Status = EnumStatusResposta.VALIDACAO_REJEITADA;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<List<ReadPagamentoContaDTO>> ObterPagamentosConta(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadPagamentoContaDTO>>();
                busca = busca ?? string.Empty;
                var model = _context.Obter(x => x.Conta.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadPagamentoContaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadPagamentoContaDTO> ObterPagamentoConta(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadPagamentoContaDTO>(model);
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

        public RespostaServico<ReadPagamentoContaDTO> EditarPagamentoConta(int id, UpdatePagamentoContaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_context.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadPagamentoContaDTO>(model);
                    retorno.Mensagem = "Registro atualizado com sucesso.";
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
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<ReadPagamentoContaDTO> ExcluirPagamentoConta(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadPagamentoContaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_context.Excluir(model) == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados..");
                    retorno.Valor = _mapper.Map<ReadPagamentoContaDTO>(model);
                    retorno.Mensagem = "Registro excluído com sucesso.";
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
