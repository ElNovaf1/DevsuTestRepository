using AutoMapper;
using Devsu.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace Devsu.API.Controllers
{
    [ApiController]
    public class ReportesController : Controller
    {
        IClientService _service;
        ILogger _Logger;
        IMapper _mapper;

        public ReportesController(IClientService service, Serilog.ILogger logger, IMapper mapper)
        {
            _service = service;
            _Logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/Reportes/{idcliente}/")]
        public async Task<IActionResult> GetReportMovements([FromRoute] int idcliente, [FromQuery(Name = "fechainicio")] DateTime? fechainicio, [FromQuery(Name = "fechafin")]  DateTime? fechafin )
        {
            try
            {
                _Logger.Information("Solicitud de reporte de cliente " + idcliente.ToString() + " recibida");
                var result = await _service.GetReporteMovimientosAsync(idcliente, fechainicio, fechafin);
                if (result == null)
                {
                    return NotFound("No se encontró registro del cliente");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al generar reporte para cliente " + idcliente.ToString() + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("api/Reportes/")]
        public async Task<IActionResult> GetReportMovementsPorNombre([FromQuery] string nombre, [FromQuery(Name = "fechainicio")] DateTime? fechainicio, [FromQuery(Name = "fechafin")] DateTime? fechafin)
        {
            try
            {
                _Logger.Information("Solicitud de reporte de cliente por nombre " + nombre + " recibida");
                var result = await _service.GetReporteMovimientosNombreAsync(nombre, fechainicio, fechafin);
                if (result == null)
                {
                    return NotFound("No se encontró registro del cliente");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.Error("Error al generar reporte para cliente por nombre " + nombre + ": " + ex.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}
