using AutoMapper;
using Vendinha.Commons.DTOs;
using Vendinha.Commons.Entities;

namespace Vendinha.Commons.Mapper
{
    public class DividaProfile : Profile
    {
        public DividaProfile()
        {
            CreateMap<DividaDto, Divida>();
            CreateMap<Divida, DividaDto>();
        }
    }
}
