using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Devsu.Service;
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
    public class ClienteController : Controller
    {
        IClientService _service;
        ILogger _Logger;
        IMapper _mapper;

        public ClienteController(IClientService service, Serilog.ILogger logger, IMapper mapper)
        {
            _service = service;
            _Logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("api/Clientes/{id}")]
        public async Task<IActionResult> GetClient([FromRoute] int id)
        {
            try
            {
                _Logger.Information("Solicitud de consultar cliente " + id.ToString() + " recibida");
                
                var result = await _service.GetByIdAsync(1);
                if (result == null)
                {
                    return NotFound("No se encontró registro del cliente");
                }
                return Ok(_mapper.Map<ClienteDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al consultar cliente " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("api/Clientes/")]
        public async Task<IActionResult> PostClient([FromBody] CreateClienteDTO datos)
        {
            try
            {
                _Logger.Information("Solicitud de crear cliente " + datos.Nombres + " recibida");

                var result = await _service.AddAsync(_mapper.Map<Cliente>(datos));

                return Ok(_mapper.Map<ClienteDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Information("Error al crear cliente " + datos.Nombres + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("api/Clientes/{id}")]
        public async Task<IActionResult> PutClient([FromRoute] int id, [FromBody] UpdateClienteDTO datos)
        {
            try
            {
                _Logger.Information("Solicitud de actualizar cliente " + id.ToString() + " recibida");
                var result = await _service.UpdateAsync(id, _mapper.Map<Cliente>(datos));
                if (result == null)
                {
                    return NotFound("No se encontró registro del cliente a actualizar");
                }
                return Ok(_mapper.Map<ClienteDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al actualizar cliente " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("api/Clientes/{id}")]
        public async Task<IActionResult> PatchClient([FromRoute] int id, [FromBody] JsonPatchDocument clientDocument)
        {
            try
            {
                _Logger.Information("Solicitud de patch para cliente " + id.ToString() + " recibida");
                var result = await _service.PatchAsync(id, clientDocument);
                if (result == null)
                {
                    return NotFound("No se encontró registro del cliente a actualizar");
                }
                return Ok(_mapper.Map<ClienteDTO>(result));
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al actualizar cliente " + id.ToString() + " via patch: " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("api/Clientes/{id}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            try
            {
                _Logger.Information("Solicitud de eliminar cliente " + id.ToString() + " recibida");
                var result = await _service.DeleteAsync(id);
                if (result == null)
                {
                    return NotFound("No se encontró registro del cliente a eliminar");
                }
                return Ok("Registro de cliente eliminado con éxito");
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al eliminar cliente " + id.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
