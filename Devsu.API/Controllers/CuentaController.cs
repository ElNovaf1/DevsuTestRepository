using Microsoft.AspNetCore.Mvc;
using Devsu.Domain.Interfaces.Services;
using AutoMapper;
using Devsu.Domain.Entities;
using Devsu.Transversal.DTO.Output;
using Devsu.Transversal.DTO.Input;
using Microsoft.AspNetCore.JsonPatch;
using ILogger = Serilog.ILogger;

namespace Devsu.API.Controllers
{
    [ApiController]
    public class CuentaController : Controller
    {
        IAccountService _service;
        ILogger _Logger;
        IMapper _mapper;

        public CuentaController(IAccountService service, Serilog.ILogger logger, IMapper mapper)
        {
            _service = service;
            _Logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/Cuentas/{id}")]
        public async Task<IActionResult> GetAccount([FromRoute] int id)
        {
            try
            {
                _Logger.Information("Solicitud de consultar cuenta " + id.ToString() + " recibida");
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound("No se encontró registro de la cuenta");
                }
                return Ok(_mapper.Map<CuentaDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al consultar cuenta " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("api/Cuentas/{id}/Movimientos/")]
        public async Task<IActionResult> GetAccountMovements([FromRoute] int id)
        {
            try
            {
                _Logger.Information("Solicitud de consultar movimientos de cuenta " + id.ToString() + " recibida");
                var result = await _service.GetMovementsByIdAsync(id);
                if (result == null)
                {
                    return NotFound("No se encontró registro de la cuenta");
                }
                return Ok(_mapper.Map<CuentaMovimientosDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al consultar movimientos de cuenta " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("api/Cuentas/")]
        public async Task<IActionResult> PostAccount([FromBody] CreateCuentaDTO datos)
        {
            try
            {
                _Logger.Information("Solicitud de crear cuenta con numero " + datos.Numero + " recibida");
                var result = await _service.AddAsync(_mapper.Map<Cuenta>(datos));

                return Ok(_mapper.Map<CuentaDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Information("Error al crear cuenta con numero " + datos.Numero + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("api/Cuentas/{id}")]
        public async Task<IActionResult> PutAccount([FromRoute] int id, [FromBody] UpdateCuentaDTO datos)
        {
            try
            {
                _Logger.Information("Solicitud de actualizar cuenta " + id.ToString() + " recibida");
                var result = await _service.UpdateAsync(id, _mapper.Map<Cuenta>(datos));
                if (result == null)
                {
                    return NotFound("No se encontró registro de la cuenta a actualizar");
                }
                return Ok(_mapper.Map<CuentaDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al actualizar cuenta " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("api/Cuentas/{id}")]
        public async Task<IActionResult> PatchAccount([FromRoute] int id, [FromBody] JsonPatchDocument clientDocument)
        {
            try
            {
                _Logger.Information("Solicitud de patch para cuenta " + id.ToString() + " recibida");
                var result = await _service.PatchAsync(id, clientDocument);
                if (result == null)
                {
                    return NotFound("No se encontró registro de la cuenta a actualizar");
                }
                return Ok(_mapper.Map<CuentaDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al actualizar cuenta " + id.ToString() + " via patch: " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("api/Cuentas/{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            try
            {
                _Logger.Information("Solicitud de eliminar cuenta " + id.ToString() + " recibida");
                var result =  await _service.DeleteAsync(id);
                if (result == null)
                {
                    return NotFound("No se encontró registro de la cuenta a actualizar");
                }
                return Ok("Registro de cuenta eliminado con éxito");
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al eliminar cuenta " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
