using AutoMapper;
using Devsu.Domain.Entities;
using Devsu.Transversal.DTO.Input;
using Devsu.Transversal.DTO.Output;
namespace Devsu.API.Profiles
{
    public class CuentaProfile : Profile
    {
        public CuentaProfile()
        {
            CreateMap<Cuenta, CuentaDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
            .ForMember(dest => dest.TipoCuenta, opt => opt.MapFrom(src => src.TipoCuenta))
            .ForMember(dest => dest.SaldoInicial, opt => opt.MapFrom(src => src.SaldoInicial))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente.Nombre));

            CreateMap<Cuenta, CuentaMovimientosDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
            .ForMember(dest => dest.TipoCuenta, opt => opt.MapFrom(src => src.TipoCuenta))
            .ForMember(dest => dest.SaldoInicial, opt => opt.MapFrom(src => src.SaldoInicial))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.Movimientos, opt => opt.MapFrom(src => src.Movimientos))
            .ForMember(dest => dest.SaldoActual, opt => opt.MapFrom(src => src.Movimientos.Sum(s => s.Valor)))
            .ForMember(dest => dest.TotalCreditos, opt => opt.MapFrom(src => src.Movimientos.Where(p => p.Valor > 0).Sum(s => s.Valor)))
            .ForMember(dest => dest.TotalDebitos, opt => opt.MapFrom(src => src.Movimientos.Where(p => p.Valor < 0).Sum(s => s.Valor)));

            CreateMap<Cuenta, CreateCuentaDTO>()
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
            .ForMember(dest => dest.TipoCuenta, opt => opt.MapFrom(src => src.TipoCuenta))
            .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.SaldoInicial, opt => opt.MapFrom(src => src.SaldoInicial))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ReverseMap();

            CreateMap<Cuenta, UpdateCuentaDTO>()
            .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
            .ForMember(dest => dest.TipoCuenta, opt => opt.MapFrom(src => src.TipoCuenta))
            .ForMember(dest => dest.NombreCliente, opt => opt.MapFrom(src => src.Cliente.Nombre))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
            .ReverseMap();

        }
    }
}
