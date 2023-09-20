
namespace Devsu.Transversal.DTO.Output
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public bool Estado { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public byte? Edad { get; set; }

        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Genero { get; set; }
    }
}
