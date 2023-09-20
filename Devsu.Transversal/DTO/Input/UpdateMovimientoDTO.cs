using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Input
{
    public class UpdateMovimientoDTO
    {
        [Required(ErrorMessage = "Valor es requerido")]
        public decimal Valor { get; set; }
    }
}
