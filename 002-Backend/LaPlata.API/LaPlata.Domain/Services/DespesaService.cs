using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IContext<Despesa> _context;
        private readonly IContext<Log> _contextLogErro;
        private readonly IMapper _mapper;

        public DespesaService(IContext<Despesa> context, IContext<Log> contextLogErro, IMapper mapper)
        {
            _context = context;
            _contextLogErro = contextLogErro;
            _mapper = mapper;
        }

        public RespostaServico<ReadDespesaDTO> IncluirDespesa(CreateDespesaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadDespesaDTO>();
                var model = _mapper.Map<Despesa>(DTO);
                if (_context.Adicionar(model) == 0)
                    throw new Exception("Nenhum registro incluído no banco de dados.");
                retorno.Valor = _mapper.Map<ReadDespesaDTO>(model);
                retorno.Mensagem = "Registro incluído com sucesso.";
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<List<ReadDespesaDTO>> ObterDespesas(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadDespesaDTO>>();
                busca = busca ?? string.Empty;
                var model = _context.Obter(x => x.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadDespesaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadDespesaDTO> ObterDespesa(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadDespesaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadDespesaDTO>(model);
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
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadDespesaDTO> EditarDespesa(int id, UpdateDespesaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadDespesaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_context.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadDespesaDTO>(model);
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
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<ReadDespesaDTO> ExcluirDespesa(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadDespesaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_context.Excluir(model) == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadDespesaDTO>(model);
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
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }
    }
}
