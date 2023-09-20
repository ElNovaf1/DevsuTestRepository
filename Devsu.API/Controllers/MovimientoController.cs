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
    public class MovimientoController : Controller
    {
        IMovementService _service;
        ILogger _Logger;
        IMapper _mapper;
        IConfiguration _config;

        public MovimientoController(IMovementService service, Serilog.ILogger logger, IMapper mapper, IConfiguration config)
        {
            _service = service;
            _Logger = logger;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        [Route("api/Movimientos/{id}")]
        public async Task<IActionResult> GetMovement([FromRoute] long id)
        {
            try
            {
                _Logger.Information("Solicitud de consultar Movimiento " + id.ToString() + " recibida");
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound("No se encontró registro del Movimiento");
                }
                return Ok(_mapper.Map<MovimientoDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al consultar movimiento " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("api/Movimientos/")]
        public async Task<IActionResult> PostMovement([FromBody] CreateMovimientoDTO datos)
        {
            try
            {

                _Logger.Information("Solicitud de crear movimiento con numero de cuenta " + datos.NumeroCuenta + " recibida");
                var result = await _service.AddAsync(_mapper.Map<Movimiento>(datos));

                return Ok(_mapper.Map<MovimientoDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Information("Error al crear cuenta movimiento con numero de cuenta " + datos.NumeroCuenta + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("api/Movimientos/{id}")]
        public async Task<IActionResult> PutMovement([FromRoute] long id, [FromBody] UpdateMovimientoDTO datos)
        {
            try
            {
                _Logger.Information("Solicitud de actualizar movimiento " + id.ToString() + " recibida");
                var result = await _service.UpdateAsync(id, _mapper.Map<Movimiento>(datos));
                if (result == null)
                {
                    return NotFound("No se encontró registro del movimiento a actualizar");
                }
                return Ok(_mapper.Map<MovimientoDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al actualizar movimiento " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("api/Movimientos/{id}")]
        public async Task<IActionResult> PatchMovement([FromRoute] long id, [FromBody] JsonPatchDocument clientDocument)
        {
            try
            {
                _Logger.Information("Solicitud de patch para movimiento " + id.ToString() + " recibida");
                var result = await _service.PatchAsync(id, clientDocument);
                if (result == null)
                {
                    return NotFound("No se encontró registro del movimiento a actualizar");
                }
                return Ok(_mapper.Map<MovimientoDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al actualizar movimiento " + id.ToString() + " via patch: " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("api/Movimientos/{id}")]
        public async Task<IActionResult> DeleteMovement([FromRoute] long id)
        {
            try
            {
                _Logger.Information("Solicitud de eliminar movimiento " + id.ToString() + " recibida");
                var result = await _service.DeleteAsync(id);
                if (result == null)
                {
                    return NotFound("No se encontró registro del movimiento a actualizar");
                }
                return Ok("Registro de movimiento eliminado con éxito");
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al eliminar movimiento " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
