using AutoMapper;
using PeliculasApii.DTOs;
using PeliculasApii.Entidades;

namespace PeliculasApii.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();
        }
    }
}
