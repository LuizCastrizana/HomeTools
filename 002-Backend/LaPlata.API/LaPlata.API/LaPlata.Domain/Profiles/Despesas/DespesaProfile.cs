using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateDespesaDTO, Despesa>();
            CreateMap<Despesa, ReadDespesaDTO>();
            CreateMap<UpdateDespesaDTO, Despesa>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Despesa, UpdateDespesaDTO>();
        }
    }
}