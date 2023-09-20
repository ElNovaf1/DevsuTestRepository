using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Output
{
    public class CuentaDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;

        public string Cliente { get; set; } = string.Empty;

        public string TipoCuenta { get; set; } = string.Empty;

        public decimal SaldoInicial { get; set; }

        public bool Estado { get; set; }
    }
}
