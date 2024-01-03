using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class CompraService : ICompraService
    {
        private readonly IContext<Compra> _context;
        private readonly IContext<Cartao> _contextCartao;
        private readonly IContext<Log> _contextLogErro;
        private readonly IMapper _mapper;

        public CompraService(IContext<Compra> context, IContext<Cartao> contextCartao, IContext<Log> contextLogErro, IMapper mapper)
        {
            _context = context;
            _contextCartao = contextCartao;
            _contextLogErro = contextLogErro;
            _mapper = mapper;
        }

        public RespostaServico<ReadCompraDTO> IncluirCompra(CreateCompraDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _mapper.Map<Compra>(DTO);
                var modelCartao = _contextCartao.Obter(x => x.Id == model.CartaoId).FirstOrDefault();
                if (modelCartao != null)
                {
                    if (_context.Adicionar(model) == 0)
                        throw new Exception("Nenhum registro incluído no banco de dados.");
                    model.Cartao = modelCartao;
                    retorno.Valor = _mapper.Map<ReadCompraDTO>(model);
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
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<List<ReadCompraDTO>> ObterCompras(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadCompraDTO>>();
                busca = busca ?? string.Empty;
                var model = _context.Obter(x => x.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadCompraDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<List<ReadCompraDTO>> ObterCompras(Expression<Func<Compra, bool>> predicate)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadCompraDTO>>();
                var model = _context.Obter(predicate).ToList();
                retorno.Valor = _mapper.Map<List<ReadCompraDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _contextLogErro.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadCompraDTO> ObterCompra(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    retorno.Valor = _mapper.Map<ReadCompraDTO>(model);
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

        public RespostaServico<ReadCompraDTO> EditarCompra(int id, UpdateCompraDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_context.SalvarAlteracoes() == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadCompraDTO>(model);
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

        public RespostaServico<ReadCompraDTO> ExcluirCompra(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _context.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_context.Excluir(model) == 0)
                        throw new Exception("Nenhum registro alterado no banco de dados.");
                    retorno.Valor = _mapper.Map<ReadCompraDTO>(model);
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
