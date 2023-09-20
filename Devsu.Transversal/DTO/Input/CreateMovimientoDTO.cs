using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Input
{
    public class CreateMovimientoDTO
    {
        [Required(ErrorMessage = "Numero de cuenta es requerido")]
        [StringLength(20)]
        [MinLength(1)]
        public string NumeroCuenta { get; set; } = string.Empty;

        [Required(ErrorMessage = "Valor es requerido")]
        public decimal Valor { get; set; }
    }
}
