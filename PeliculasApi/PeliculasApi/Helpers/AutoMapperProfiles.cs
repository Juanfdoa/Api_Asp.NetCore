
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles( GeometryFactory geometryFactory)
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreacionDTO, Genero>();

            CreateMap<Review, ReviewDTO>()
                .ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.Usuario.UserName));
            CreateMap<ReviewDTO, Review>();
            CreateMap<ReviewCreacionDTO, Review>();

            CreateMap<IdentityUser, UsuarioDTO>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreacionDTO, Actor>().ForMember(x => x.Foto, options => options.Ignore());
            CreateMap<ActorPatchDTO, Actor>().ReverseMap();

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.PeliculasGeneros, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.PeliculasActores, options => options.MapFrom(MapPeliculasActores));

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros1))
                .ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores1));

            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();

            CreateMap<SalaDeCine, SalaDeCineDTO>()
                .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));

            CreateMap<SalaDeCineDTO, SalaDeCine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
                
            CreateMap<SalaDeCineCreacionDTO, SalaDeCine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));
        }

        private List<GeneroDTO> MapPeliculasGeneros1(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<GeneroDTO>();
            if(pelicula.PeliculasGeneros == null) { return resultado; }

            foreach(var generoPelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDTO() { Id = generoPelicula.GeneroId, Nombre = generoPelicula.Genero.Nombre });
            }

            return resultado;
        }

        private List<ActorPeliculaDetalleDTO> MapPeliculasActores1(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();
            if(pelicula.PeliculasActores == null) { return resultado; }

            foreach(var actorPelicula in pelicula.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO() { ActorId = actorPelicula.ActorId, Personaje = actorPelicula.Personaje, NombrePersona = actorPelicula.Actor.Nombre }) ;
            }
            return resultado;
        }

        private List<PeliculaGenero> MapPeliculasGeneros(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculaGenero>();
            if(peliculaCreacionDTO.GenerosIDs == null) { return resultado; }

            foreach(var id in peliculaCreacionDTO.GenerosIDs)
            {
                resultado.Add(new PeliculaGenero() { GeneroId = id });
            }

            return resultado;
        }

        private List<PeliculaActor> MapPeliculasActores(PeliculaCreacionDTO peliculaCreacionDTO, Pelicula pelicula)
        {
            var resultado = new List<PeliculaActor>();
            if(peliculaCreacionDTO.Actores == null) { return resultado; }

            foreach (var actor in peliculaCreacionDTO.Actores)
            {
                resultado.Add(new PeliculaActor() { ActorId = actor.ActorId, Personaje= actor.Personaje });
            }

            return resultado;
        }
    }
}
