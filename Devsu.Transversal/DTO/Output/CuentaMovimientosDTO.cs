using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Output
{
    public class CuentaMovimientosDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; } = null!;

        public string Cliente { get; set; } = string.Empty;

        public string TipoCuenta { get; set; } = string.Empty;

        public decimal SaldoInicial { get; set; }

        public decimal SaldoActual { get; set; }

        public decimal TotalCreditos { get; set; }

        public decimal TotalDebitos { get; set; }

        public bool Estado { get; set; }

        public ICollection<DetalleMovimientoDTO> Movimientos { get; set; }
    }
}
