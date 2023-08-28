using AutoMapper;
using LaPlata.Domain.DTOs;
using LaPlata.Domain.Models;

namespace LaPlata.Domain.Profiles
{
    public class CartaoProfile : Profile
    {
        public CartaoProfile()
        {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<CreateCartaoDTO, Cartao>();
            CreateMap<Cartao, ReadCartaoDTO>();
            CreateMap<UpdateCartaoDTO, Cartao>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Cartao, UpdateCartaoDTO>();
        }
    }
}
