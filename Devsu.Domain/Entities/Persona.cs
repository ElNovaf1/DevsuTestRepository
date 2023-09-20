using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Devsu.Domain.Entities;

public partial class Persona : EntityBase<int>
{
    [Required(ErrorMessage = "Nombre es requerido")]
    [StringLength(100)]
    [MinLength(1)]
    public string Nombre { get; set; } = null!;
    public byte? Edad { get; set; }
    public string? Direccion { get; set; } = null;
    public string? Telefono { get; set; } = null;
    public string? Genero { get; set; }
}
