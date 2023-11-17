using AutoMapper;
using Template.Domain2.Dtos;
using Template.Domain2.Entities;

namespace Template.API2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteForCreationDto, Cliente>();

            CreateMap<Alquiler, AlquilerDtoForCreation>();
            CreateMap<AlquilerDtoForCreation, Alquiler>();

            CreateMap<Libro, LibroDto>();
            CreateMap<LibroDto, Libro>();

        }
    }
}
