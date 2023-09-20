using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Input
{
    public class CreateCuentaDTO
    {
        [Required(ErrorMessage = "Numero de cuenta es requerido")]
        [StringLength(20)]
        public string Numero { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Tipo de cuenta es requerido")]
        [StringLength(20)]
        public string TipoCuenta { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Nombre de cliente es requerido")]
        [StringLength(100)]
        public string NombreCliente { get; set; } = string.Empty;
       
        [Required(ErrorMessage = "Saldo inicial es requerido")]
        [Range(0.0, Double.MaxValue, ErrorMessage = "El saldo inicial debe ser igual o mayor o igual que cero")]
        public decimal SaldoInicial { get; set; }
       
        [Required(ErrorMessage = "estado de cuenta es requerido")]
        public bool Estado { get; set; }
    }
}
