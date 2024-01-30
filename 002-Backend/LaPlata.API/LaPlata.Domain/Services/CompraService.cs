using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Enums;
using LaPlata.Domain.Interfaces;
using LaPlata.Domain.Interfaces.Repositories.Cartoes;
using LaPlata.Domain.Models;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;

namespace LaPlata.Domain.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;
        private readonly ICartaoRepository _cartaoRepository;
        private readonly IRepository<Log> _logErroRepository;
        private readonly IMapper _mapper;

        public CompraService(ICompraRepository compraRepository, ICartaoRepository cartaoRepository, IRepository<Log> logErroRepository, IMapper mapper)
        {
            _compraRepository = compraRepository;
            _cartaoRepository = cartaoRepository;
            _logErroRepository = logErroRepository;
            _mapper = mapper;
        }

        public RespostaServico<ReadCompraDTO> IncluirCompra(CreateCompraDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _mapper.Map<Compra>(DTO);
                var modelCartao = _cartaoRepository.Obter(x => x.Id == model.CartaoId).FirstOrDefault();
                if (modelCartao != null)
                {
                    if (_compraRepository.Adicionar(model) == 0)
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
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<List<ReadCompraDTO>> ObterCompras(string? busca)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadCompraDTO>>();
                busca = busca ?? string.Empty;
                var model = _compraRepository.Obter(x => x.Descricao.ToUpper().Contains(busca.ToUpper())).ToList();
                retorno.Valor = _mapper.Map<List<ReadCompraDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<List<ReadCompraDTO>> ObterCompras(Expression<Func<Compra, bool>> predicate)
        {
            try
            {
                var retorno = new RespostaServico<List<ReadCompraDTO>>();
                var model = _compraRepository.Obter(predicate).ToList();
                retorno.Valor = _mapper.Map<List<ReadCompraDTO>>(model);
                retorno.Status = EnumStatusResposta.SUCESSO;
                return retorno;
            }
            catch (Exception ex)
            {
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadCompraDTO> ObterCompra(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _compraRepository.Obter(x => x.Id == id).FirstOrDefault();
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
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }

        public RespostaServico<ReadCompraDTO> EditarCompra(int id, UpdateCompraDTO DTO)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _compraRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    _mapper.Map(DTO, model);
                    if (_compraRepository.SalvarAlteracoes() == 0)
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
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message, JsonConvert.SerializeObject(DTO)));
                throw;
            }
        }

        public RespostaServico<ReadCompraDTO> ExcluirCompra(int id)
        {
            try
            {
                var retorno = new RespostaServico<ReadCompraDTO>();
                var model = _compraRepository.Obter(x => x.Id == id).FirstOrDefault();
                if (model != null)
                {
                    if (_compraRepository.Excluir(model) == 0)
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
                _logErroRepository.Adicionar(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message));
                throw;
            }
        }
    }
}
