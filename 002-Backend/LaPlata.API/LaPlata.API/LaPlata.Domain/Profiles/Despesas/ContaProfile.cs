using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class ContaProfile : Profile
    {
        public ContaProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateContaDTO, Conta>();
            CreateMap<Conta, ReadContaDTO>();
            CreateMap<UpdateContaDTO, Conta>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Conta, UpdateContaDTO>();
        }
    }
}