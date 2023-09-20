using System.ComponentModel.DataAnnotations;

namespace Devsu.Transversal.DTO.Input
{
    public class UpdateClienteDTO
    {
        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(100)]
        [MinLength(1)]
        public string Nombres { get; set; } = string.Empty;

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        [Required(ErrorMessage = "Password es requerido")]
        [StringLength(200)]
        [MinLength(1)]
        public string Contraseña { get; set; } = string.Empty;
        public byte? Edad { get; set; }

        public string? Genero { get; set; }
        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }
    }
}
