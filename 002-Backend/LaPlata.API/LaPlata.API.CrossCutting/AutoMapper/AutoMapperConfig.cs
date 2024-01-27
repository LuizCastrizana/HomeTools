using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.API.CrossCutting.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            #region Cartões

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateCartaoDTO, Cartao>();
            CreateMap<Cartao, ReadCartaoDTO>();
            CreateMap<UpdateCartaoDTO, Cartao>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Cartao, UpdateCartaoDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateAssinaturaDTO, Assinatura>();
            CreateMap<Assinatura, ReadAssinaturaDTO>();
            CreateMap<UpdateAssinaturaDTO, Assinatura>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Assinatura, UpdateAssinaturaDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateCompraDTO, Compra>();
            CreateMap<Compra, ReadCompraDTO>();
            CreateMap<UpdateCompraDTO, Compra>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Compra, UpdateCompraDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Fatura, ReadFaturaDTO>();
            CreateMap<CreateFaturaDTO, Fatura>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<AssinaturaFatura, AssinaturaFaturaDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CompraFatura, CompraFaturaDTO>();

            #endregion

            #region Despesas

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateDespesaDTO, Despesa>();
            CreateMap<Despesa, ReadDespesaDTO>();
            CreateMap<UpdateDespesaDTO, Despesa>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Despesa, UpdateDespesaDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateContaDTO, Conta>();
            CreateMap<Conta, ReadContaDTO>();
            CreateMap<UpdateContaDTO, Conta>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Conta, UpdateContaDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateContaVariavelDTO, ContaVariavel>();
            CreateMap<ContaVariavel, ReadContaVariavelDTO>();
            CreateMap<UpdateContaVariavelDTO, ContaVariavel>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ContaVariavel, UpdateContaVariavelDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreatePagamentoContaDTO, PagamentoConta>();
            CreateMap<PagamentoConta, ReadPagamentoContaDTO>();
            CreateMap<UpdatePagamentoContaDTO, PagamentoConta>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PagamentoConta, UpdatePagamentoContaDTO>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreatePagamentoContaVariavelDTO, PagamentoContaVariavel>();
            CreateMap<PagamentoContaVariavel, ReadPagamentoContaVariavelDTO>();
            CreateMap<UpdatePagamentoContaVariavelDTO, PagamentoContaVariavel>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PagamentoContaVariavel, UpdatePagamentoContaVariavelDTO>();

            #endregion

            #region Categorias

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Categoria, ReadCategoriaDTO>();
            CreateMap<CreateCategoriaDTO, Categoria>();

            #endregion
        }

    }
}
