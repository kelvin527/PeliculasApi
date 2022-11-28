using AutoMapper;
using PeliculasApi.Dtos;
using PeliculasApi.Entidades;

namespace PeliculasApi.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Genero, GenerosDto>().ReverseMap();
            CreateMap<AgregarGeneroDto, Genero>();

            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<AgregarActorDto, Actor>();
        }
    }
}
