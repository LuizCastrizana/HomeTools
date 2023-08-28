using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class PagamentoContaProfile : Profile
    {
        public PagamentoContaProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreatePagamentoContaDTO, PagamentoConta>();
            CreateMap<PagamentoConta, ReadPagamentoContaDTO>();
            CreateMap<UpdatePagamentoContaDTO, PagamentoConta>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PagamentoConta, UpdatePagamentoContaDTO>();
        }
    }
}