using Devsu.Domain.Entities;
using Devsu.Domain.Interfaces;
using Devsu.Domain.Interfaces.Services;
using Devsu.Transversal.DTO.Input;
using Microsoft.AspNetCore.JsonPatch;

namespace Devsu.Service
{
    public class AccountService :IAccountService
    {
        private readonly IWorkUnit _WorkUnit;
        public AccountService(IWorkUnit workUnit)
        {
            _WorkUnit = workUnit;
        }

        public async Task<IList<Cuenta>> GetAllAsync()
        {
            return await _WorkUnit.Repository<Cuenta>().GetAllAsync();
        }

        public async Task<Cuenta> GetByIdAsync(int Id)
        {
            return await _WorkUnit.Repository<Cuenta>().GetByIdAsync(Id, x=> x.Cliente);
        }

        public async Task<Cuenta> GetMovementsByIdAsync(int Id)
        {
            return await _WorkUnit.Repository<Cuenta>().GetByIdAsync(Id,x=> x.Movimientos, x=> x.Cliente);
        }
        public async Task<Cuenta> AddAsync(Cuenta entity)
        {
            try
            {
                await _WorkUnit.Begin();

                var sCliente = await _WorkUnit.Repository<Cliente>().GetByFilterAsync(x => x.Nombre == entity.Cliente.Nombre);
                if (sCliente.Count() == 0)
                {
                    throw new KeyNotFoundException("No se encontró registro para el cliente indicado");
                }
                else
                {
                    var sCuenta = await _WorkUnit.Repository<Cuenta>().GetByFilterAsync(x => x.Numero == entity.Numero);
                    if (sCuenta.Count() == 0)
                    {
                        entity.Cliente = sCliente.FirstOrDefault()!;
                        var newCuenta = await _WorkUnit.Repository<Cuenta>().AddAsync(entity);

                        // INSERTAR MOVIMIENTO CON EL MONTO INICIAL
                        if (newCuenta.SaldoInicial > 0) {
                            string tipoMov = newCuenta.SaldoInicial >= 0 ? "Crédito" : "Débito";
                            var datanewMovimiento = new Movimiento()
                            {
                                CuentaId = newCuenta.Id,
                                TipoMovimiento = tipoMov,
                                Fecha = DateTime.Now,
                                Valor = newCuenta.SaldoInicial,
                                Saldo = newCuenta.SaldoInicial
                            };
                            var newMovimiento = await _WorkUnit.Repository<Movimiento>().AddAsync(datanewMovimiento);
                        }
                        await _WorkUnit.Commit();
                        return newCuenta;
                    } else {
                        throw new Exception("Ya existe una cuenta registrada con ese numero");
                    }
                }
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }
        public async Task<Cuenta> UpdateAsync(int id, Cuenta entity)
        {
            try
            {
                await _WorkUnit.Begin();
                var sCliente = await _WorkUnit.Repository<Cliente>().GetByFilterAsync(x => x.Nombre == entity.Cliente.Nombre);
                if (sCliente.Count() == 0)
                {
                    throw new KeyNotFoundException("No se encontró registro para el cliente indicado");
                }
                var sCuenta = await _WorkUnit.Repository<Cuenta>().GetByIdAsync(id);
                if (sCuenta == null)
                {
                    await _WorkUnit.Rollback();
                    return sCuenta;
                }
                var cCuenta = sCuenta!;
                cCuenta.TipoCuenta = entity.TipoCuenta;
                cCuenta.Cliente = sCliente.FirstOrDefault()!;
                cCuenta.Estado = entity.Estado;

                var updatedCuenta = await _WorkUnit.Repository<Cuenta>().UpdateAsync(cCuenta);
                await _WorkUnit.Commit();
                return updatedCuenta;
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }
        public async Task<Cuenta> PatchAsync(int id, JsonPatchDocument jsonEntity)
        {
            try
            {
                await _WorkUnit.Begin();
                var sCuenta = await _WorkUnit.Repository<Cuenta>().GetByIdAsync(id);
                if (sCuenta == null)
                {
                    return sCuenta;
                }
                else
                {
                    //TODO: VALIDAR QUE EXISTA CUENTA QUE SE RECIBE EN EL PATCH
                    // TODO: VALIDAR INFORMACIÓN RECIBIDA EN PATCH
                    var updatedCuenta = await _WorkUnit.Repository<Cuenta>().PatchAsync(x => x.Id == id, jsonEntity, x=> x.Cliente);
                    await _WorkUnit.Commit();
                    return updatedCuenta;
                }
            }
            catch (Exception ex)
            {
                await _WorkUnit.Rollback();
                throw;
            }
        }

        public async Task<Cuenta> DeleteAsync(int id)
        {
            try
            {
                await _WorkUnit.Begin();
                var sCuenta = await _WorkUnit.Repository<Cuenta>().GetByIdAsync(id,x=> x.Movimientos);
                if (sCuenta == null)
                {
                    return sCuenta;
                }
                else
                {
                    await _WorkUnit.Repository<Cuenta>().DeleteAsync(sCuenta.Id);
                    await _WorkUnit.Commit();
                    return sCuenta;
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
