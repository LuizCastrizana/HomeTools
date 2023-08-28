using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class AssinaturaFaturaProfile : Profile
    {
        public AssinaturaFaturaProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<AssinaturaFatura, AssinaturaFaturaDTO>();
        }
    }
}
