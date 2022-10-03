using AutoMapper;
using CardStorageService.Data;
using SecureDevelopment.Models;
using SecureDevelopment.Models.Requests;

namespace SecureDevelopment.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Card, CardDto>();
            CreateMap<CreateCardRequest, Card>();

            CreateMap<Client, ClientDto>();
            CreateMap<CreateClientRequest, Client>();
        }
    }
}
