using AutoMapper;
using Vendinha.Commons.DTOs;
using Vendinha.Commons.Entities;

namespace Vendinha.Commons.Mapper
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteDto, Cliente>();
            CreateMap<Cliente, ClienteDto>();
        }
    }
}
