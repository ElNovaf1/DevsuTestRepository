using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Output
{
    public class MovimientoDTO
    {
        public long Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public String Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
    }
}
