using AutoMapper;
using Devsu.Domain.Entities;
using Devsu.Transversal.DTO.Input;
using Devsu.Transversal.DTO.Output;

namespace Devsu.API.Profiles
{
    public class MovimientoProfile : Profile
    {

        public MovimientoProfile()
        {
            CreateMap<Movimiento, MovimientoDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.NumeroCuenta, opt => opt.MapFrom(src => src.Cuenta.Numero))
            .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoMovimiento))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            .ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.Saldo));

            CreateMap<Movimiento, DetalleMovimientoDTO>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
           .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.TipoMovimiento))
           .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
           .ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => src.Saldo));

            CreateMap<Movimiento, CreateMovimientoDTO>()
            .ForMember(dest => dest.NumeroCuenta, opt => opt.MapFrom(src => src.Cuenta.Numero))
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            .ReverseMap();

            CreateMap<Movimiento, UpdateMovimientoDTO>()
            .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
            .ReverseMap();
        }
    }
}
