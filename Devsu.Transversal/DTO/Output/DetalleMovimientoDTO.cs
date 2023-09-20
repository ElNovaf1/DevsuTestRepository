using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Output
{
    public class DetalleMovimientoDTO
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public String Tipo { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
