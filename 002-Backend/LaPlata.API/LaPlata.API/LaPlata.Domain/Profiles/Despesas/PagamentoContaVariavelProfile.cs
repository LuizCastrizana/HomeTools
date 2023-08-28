using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class PagamentoContaVariavelProfile : Profile
    {
        public PagamentoContaVariavelProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreatePagamentoContaVariavelDTO, PagamentoContaVariavel>();
            CreateMap<PagamentoContaVariavel, ReadPagamentoContaVariavelDTO>();
            CreateMap<UpdatePagamentoContaVariavelDTO, PagamentoContaVariavel>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<PagamentoContaVariavel, UpdatePagamentoContaVariavelDTO>();
        }
    }
}