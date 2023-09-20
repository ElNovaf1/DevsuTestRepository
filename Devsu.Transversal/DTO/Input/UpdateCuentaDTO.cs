using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devsu.Transversal.DTO.Input
{
    public class UpdateCuentaDTO
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

        [Required(ErrorMessage = "estado de cuenta es requerido")]
        public bool Estado { get; set; }
    }
}
