using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using System.Reflection;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace LaPlata.Domain.Services
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly IContext<Assinatura> _context;
        private readonly IContext<Cartao> _contextCartao;
        private readonly IContext<LogErro> _contextLogErro;
        private readonly IMapper _mapper;

        public AssinaturaService(IContext<Assinatura> context, IContext<Cartao> contextCartao, IContext<LogErro> contextLogErro, IMapper mapper)
        {
            _context = context;
            _contextCartao = contextCartao;
            _contextLogErro = contextLogErro;
            _mapper = mapper;
        }

        public RespostaServico<ReadAssinaturaDTO> IncluirAssinatura(CreateAssinaturaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _mapper.Map<Assinatura>(DTO);
                var modelCartao = _contextCartao.Obter(x => x.Id == model.CartaoId).FirstOrDefault();
                if (modelCartao != null)
                {
                    if (_context.Adicionar(model) == 0)
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
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<List<ReadAssinaturaDTO>> ObterAssinaturas(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadAssinaturaDTO>>();
                busca = busca ?? string.Empty;
                var model = _context.Obter(x => x.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadAssinaturaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<List<ReadAssinaturaDTO>> ObterAssinaturas(Expression<Func<Assinatura, bool>> predicate)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadAssinaturaDTO>>();
                var model = _context.Obter(predicate).ToList();
                retorno.Valor = _mapper.Map<List<ReadAssinaturaDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadAssinaturaDTO> ObterAssinatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
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
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadAssinaturaDTO> EditarAssinatura(int id, UpdateAssinaturaDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_context.SalvarAlteracoes() == 0)
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
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<ReadAssinaturaDTO> ExcluirAssinatura(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadAssinaturaDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_context.Excluir(model) == 0)
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
                _contextLogErro.Adicionar(new LogErro(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }
    }
}
