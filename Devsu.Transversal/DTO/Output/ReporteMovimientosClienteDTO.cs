using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Output
{
    public class ReporteMovimientosClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public byte? Edad { get; set; }

        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Genero { get; set; }

        public ICollection<CuentaMovimientosDTO> Cuentas { get; set; }
    }
}
