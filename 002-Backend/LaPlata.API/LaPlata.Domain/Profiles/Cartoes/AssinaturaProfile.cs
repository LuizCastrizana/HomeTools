using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class AssinaturaProfile : Profile
    {
        public AssinaturaProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateAssinaturaDTO, Assinatura>();
            CreateMap<Assinatura, ReadAssinaturaDTO>();
            CreateMap<UpdateAssinaturaDTO, Assinatura>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Assinatura, UpdateAssinaturaDTO>();
        }
    }
}
