using Devsu.Domain.Entities;
using Devsu.Domain.Interfaces;
using Devsu.Domain.Interfaces.Services;
using Devsu.Transversal.DTO.Input;
using HandlebarsDotNet;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Drawing;


namespace Devsu.Service
{
    public class MovementService : IMovementService
    {
        private readonly IWorkUnit _WorkUnit;
        IConfiguration _config;

        public MovementService(IWorkUnit workUnit, IConfiguration config)
        {
            _WorkUnit = workUnit;
            _config = config;
        }

        public async Task<IList<Movimiento>> GetAllAsync()
        {
            return await _WorkUnit.Repository<Movimiento>().GetAllAsync(x => x.Cuenta);
        }

        public async Task<Movimiento> GetByIdAsync(long Id)
        {
            return await _WorkUnit.Repository<Movimiento>().GetByIdAsync(Id, x => x.Cuenta);
        }
        public async Task<Movimiento> AddAsync(Movimiento entity)
        {
            try
            {
                await _WorkUnit.Begin();

                var sCuenta = await _WorkUnit.Repository<Cuenta>().GetByFilterAsync(x => x.Numero == entity.Cuenta.Numero);
                if (sCuenta.Count() == 0)
                {
                    throw new KeyNotFoundException("No se encontró registro para la cuenta indicada");
                }
                else
                {
                    var infoCuenta = sCuenta.FirstOrDefault();
                    string tipoMov = entity.Valor >= 0 ? "Crédito" : "Débito";
                    decimal sSaldo = 0;

                    //OBTENIENDO ULTIMOS SALDO PARA VALIDACIÓN
                    var ultimoMovimiento = await _WorkUnit.Repository<Movimiento>().GetLastByParamAsync("CuentaId", infoCuenta!.Id, o => o.Fecha, x => x.Fecha);
                    if (ultimoMovimiento != null)
                    {
                        sSaldo = ultimoMovimiento.Saldo;
                    }

                    if (sSaldo + entity.Valor < 0)
                    {
                        throw new InvalidOperationException("La cuenta no tiene suficiente saldo disponible para realizar la operación");
                    }

                    decimal sDebitosDia = 0;
                    decimal limiteDiario = decimal.Parse(_config["Settings:DailyWithdrawalLimit"]!); ;
                    //VALIDANDO LIMITE DIARIO DE RETIRO
                    var MovimientosDia = await _WorkUnit.Repository<Movimiento>().GetByFilterAsync(x => x.CuentaId == infoCuenta!.Id && x.Fecha.Date == DateTime.Now.Date);
                    if (MovimientosDia.Count() > 0)
                    {
                        sDebitosDia = MovimientosDia.Where(p => p.Valor < 0).Sum(v => v.Valor) * -1;
                    }

                    if (entity.Valor < 0 && sDebitosDia + (entity.Valor * -1) > limiteDiario)
                    {
                        throw new InvalidOperationException("Limite diario de retiro excedido para la cuenta ($" + limiteDiario.ToString() + ")");
                    }


                    entity.Cuenta = infoCuenta!;
                    entity.TipoMovimiento = tipoMov;
                    entity.Fecha = DateTime.Now;
                    entity.Saldo = sSaldo + entity.Valor;
                    var newMovimiento = await _WorkUnit.Repository<Movimiento>().AddAsync(entity);
                    await _WorkUnit.Commit();
                    return await _WorkUnit.Repository<Movimiento>().GetByIdAsync(newMovimiento.Id, x => x.Cuenta);
                }
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }
        public async Task<Movimiento> UpdateAsync(long id, Movimiento entity)
        {
            try
            {
                await _WorkUnit.Begin();
                var sMovimiento = await _WorkUnit.Repository<Movimiento>().GetByIdAsync(id);
                if (sMovimiento == null)
                {
                    await _WorkUnit.Rollback();
                    return sMovimiento;
                }

                //TODO: SE DEBERIA RECALCULAR EL SALDO PARA TODOS LOS MOVIMIENTOS POSTERIORES DE LA CUENTA
                //DEBERIA VALIDARSE CON NEGOCIO COMO ESTO AFECTARIA LOS MOVIMIENTOS EN CASO DE QUE QUEDASEN CON SALDO MENOR A CERO
                //REVISAR SOLUCION PROPUESTA EN STORED PROCEDURE uspActualizarSaldos, AUNQUE NO MANEJA LOS CASOS DONDE EL SALDO ES MENOR A CERO

                string tipoMov = entity.Valor >= 0 ? "Crédito" : "Débito";
                decimal sSaldo = sMovimiento.Saldo - sMovimiento.Valor + entity.Valor;

                sMovimiento.TipoMovimiento = tipoMov;
                sMovimiento.Valor = entity.Valor;
                sMovimiento.Saldo = sSaldo;

                var updatedMovimiento = await _WorkUnit.Repository<Movimiento>().UpdateAsync(sMovimiento);
                await _WorkUnit.Commit();
                return await _WorkUnit.Repository<Movimiento>().GetByIdAsync(updatedMovimiento.Id, x => x.Cuenta);
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }
        public async Task<Movimiento> PatchAsync(long id, JsonPatchDocument jsonEntity)
        {
            try
            {
                await _WorkUnit.Begin();
                var sMovimiento = await _WorkUnit.Repository<Movimiento>().GetByIdAsync(id);
                if (sMovimiento == null)
                {
                    return sMovimiento;
                }
                else
                {
                    //TODO: VALIDAR QUE EXISTA EL CLIENTE QUE SE RECIBE EN EL PATCH
                    // TODO: VALIDAR INFORMACIÓN RECIBIDA EN PATCH
                    var updatedMovimiento = await _WorkUnit.Repository<Movimiento>().PatchAsync(x => x.Id == id, jsonEntity);
                    await _WorkUnit.Commit();
                    return await _WorkUnit.Repository<Movimiento>().GetByIdAsync(updatedMovimiento.Id, x => x.Cuenta);
                }
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }

        public async Task<Movimiento> DeleteAsync(long id)
        {
            try
            {
                await _WorkUnit.Begin();
                var sMovimiento = await _WorkUnit.Repository<Movimiento>().GetByIdAsync(id);
                if (sMovimiento == null)
                {
                    return sMovimiento;
                }
                else
                {
                    //TODO: SE DEBERIAN ACTUALIZAR LOS REGISTROS POSTERIORES
                    //¿COMO AFECTA ESTO SI LOS SALDOS QUEDAN EN CERO?
                    await _WorkUnit.Repository<Movimiento>().DeleteAsync(sMovimiento.Id);
                    await _WorkUnit.Commit();
                    return sMovimiento;
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
