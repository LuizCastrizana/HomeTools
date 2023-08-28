using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class ContaVariavelProfile : Profile
    {
        public ContaVariavelProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateContaVariavelDTO, ContaVariavel>();
            CreateMap<ContaVariavel, ReadContaVariavelDTO>();
            CreateMap<UpdateContaVariavelDTO, ContaVariavel>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ContaVariavel, UpdateContaVariavelDTO>();
        }
    }
}