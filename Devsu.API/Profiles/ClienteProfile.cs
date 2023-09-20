using AutoMapper;
using Devsu.Domain.Entities;
using Devsu.Transversal.DTO.Input;
using Devsu.Transversal.DTO.Output;
using System.ComponentModel.DataAnnotations;

namespace Devsu.API.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Edad))
            .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono));

            CreateMap<Cliente, ReporteMovimientosClienteDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Edad))
            .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono));

            CreateMap<Cliente, CreateClienteDTO>()
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Edad))
            .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ReverseMap();

            CreateMap<Cliente, UpdateClienteDTO>()
            .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombre))
            .ForMember(dest => dest.Edad, opt => opt.MapFrom(src => src.Edad))
            .ForMember(dest => dest.Genero, opt => opt.MapFrom(src => src.Genero))
            .ForMember(dest => dest.Direccion, opt => opt.MapFrom(src => src.Direccion))
            .ForMember(dest => dest.Contraseña, opt => opt.MapFrom(src => src.Contraseña))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Telefono))
            .ReverseMap();
        }
    }
}