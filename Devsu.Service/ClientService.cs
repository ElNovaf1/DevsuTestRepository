using Devsu.Domain.Entities;
using Devsu.Domain.Interfaces;
using Devsu.Domain.Interfaces.Services;
using Devsu.Transversal.DTO.Input;
using Microsoft.AspNetCore.JsonPatch;
using Devsu.Transversal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Devsu.Transversal.DTO.Output;
using AutoMapper;
using System.IO;
using System.Collections;

namespace Devsu.Service
{
    public class ClientService : IClientService
    {
        private readonly IWorkUnit _WorkUnit;
        IMapper _mapper;
        public ClientService(IWorkUnit workUnit, IMapper mapper)
        {
            _WorkUnit = workUnit;
            _mapper = mapper;
        }

        //public async Task<IList<Cliente>> GetAllAsync()
        //{
        //    return await _WorkUnit.Repository<Cliente>().GetAllAsync();
        //}

        public async Task<Cliente> GetByIdAsync(int Id)
        {
            return await _WorkUnit.Repository<Cliente>().GetByIdAsync(Id);
        }

        public async Task<ReporteMovimientosClienteDTO> GetReporteMovimientosAsync(int id, DateTime? finicio, DateTime? ffin)
        {
            try
            {
                var sCliente = await _WorkUnit.Repository<Cliente>().GetByIdAsync(id, x => x.Cuentas);
                if (sCliente == null)
                {
                    throw new KeyNotFoundException("No existe registro par el cliente indicado");
                }
                else
                {
                    ReporteMovimientosClienteDTO reportData = _mapper.Map<ReporteMovimientosClienteDTO>(sCliente);
                    foreach (var item in reportData.Cuentas)
                    {
                        var sMovimientos = await _WorkUnit.Repository<Movimiento>().GetByFilterAsync(x => x.CuentaId == item.Id);
                        item.SaldoActual = sMovimientos.Sum(p => p.Valor);
                        var fMovimientos = await _WorkUnit.Repository<Movimiento>().GetByFilterAsync(x => x.CuentaId == item.Id && (finicio == null || x.Fecha >= finicio) && (ffin == null || x.Fecha <= ffin));
                        if (fMovimientos.Count() > 0)
                        {
                            item.TotalCreditos = fMovimientos.Where(p => p.Valor > 0).Sum(p => p.Valor);
                            item.TotalDebitos = fMovimientos.Where(p => p.Valor < 0).Sum(p => p.Valor);
                            item.Movimientos = _mapper.Map<ICollection<DetalleMovimientoDTO>>(fMovimientos!);
                        }
                    }
                    return reportData;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ReporteMovimientosClienteDTO> GetReporteMovimientosNombreAsync(string nombre, DateTime? finicio, DateTime? ffin)
        {
            try
            {
                var sCliente = await _WorkUnit.Repository<Cliente>().GetByFilterAsync(x => x.Nombre == nombre);
                if (sCliente.Count == 0)
                {
                    throw new KeyNotFoundException("No existe registro par el cliente indicado");
                }
                else
                {
                    var cCliente = sCliente.FirstOrDefault();
                    return await GetReporteMovimientosAsync(cCliente!.Id, finicio, ffin);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<Cliente> AddAsync(Cliente entity)
        {
            try
            {
                await _WorkUnit.Begin();
                
                var sCliente = await _WorkUnit.Repository<Cliente>().GetByFilterAsync(x => x.Nombre == entity.Nombre);
                if (sCliente.Count() == 0)
                {
                    string EncriptedPass = new Encryption().StringToSHA256(entity.Contraseña);
                    var newCliente = await _WorkUnit.Repository<Cliente>().AddAsync(entity);
                    await _WorkUnit.Commit();
                    return newCliente;
                }
                else
                {
                    throw new Exception($"{nameof(AddAsync)} ya existe un registro para el cliente: {entity.Nombre}");
                }
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }
        public async Task<Cliente> UpdateAsync(int id, Cliente entity)
        {
            try
            {
                
                await _WorkUnit.Begin();
                var sCliente = await _WorkUnit.Repository<Cliente>().GetByIdAsync(id);
                if (sCliente == null)
                {
                    await _WorkUnit.Rollback();
                    return sCliente;
                }
                else
                {
                    string EncriptedPass = new Encryption().StringToSHA256(entity.Contraseña);
                    sCliente.Estado = entity.Estado;
                    sCliente.Contraseña = EncriptedPass;
                    sCliente.Nombre = entity.Nombre;
                    sCliente.Direccion = entity.Direccion;
                    sCliente.Telefono = entity.Telefono;
                    sCliente.Edad = entity.Edad;
                    sCliente.Genero = entity.Genero;

                    var updatedCliente = await _WorkUnit.Repository<Cliente>().UpdateAsync(sCliente);
                    await _WorkUnit.Commit();
                    return updatedCliente;
                }
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }
        public async Task<Cliente> PatchAsync(int id, JsonPatchDocument jsonEntity)
        {

            try
            {
                await _WorkUnit.Begin();
                var sCliente = await _WorkUnit.Repository<Cliente>().GetByIdAsync(id);
                if (sCliente == null)
                {
                    return sCliente;
                }
                else
                {
                    // TODO: VALIDAR INFORMACIÓN RECIBIDA EN PATCH
                    var updatedCliente = await _WorkUnit.Repository<Cliente>().PatchAsync(x => x.Id == id, jsonEntity);
                    await _WorkUnit.Commit();
                    return updatedCliente;
                }
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }

        public async Task<Cliente> DeleteAsync(int id)
        {
            try
            {
                
                await _WorkUnit.Begin();
                
                var sCliente = await _WorkUnit.Repository<Cliente>().GetByIdAsync(id, x=> x.Cuentas);
                if (sCliente == null)
                {
                    return sCliente;
                }
                else
                {
                   foreach (var c in sCliente.Cuentas)
                   {
                       var movs = await _WorkUnit.Repository<Movimiento>().GetByFilterAsync(x => x.CuentaId == c.Id);
                       if (movs != null) {
                           c.Movimientos = movs;
                       }
                   }

                    await _WorkUnit.Repository<Cliente>().DeleteAsync(sCliente.Id);
                    await _WorkUnit.Commit();
                    return sCliente;
                }
            }
            catch (Exception e)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }
    }
}
