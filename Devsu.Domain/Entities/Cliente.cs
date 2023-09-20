using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Devsu.Domain.Entities;

public partial class Cliente : Persona
{
    [Required(ErrorMessage = "Password es requerido")]
    [StringLength(200)]
    [MinLength(1)]
    public string Contraseña { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Estado es requerido")]
    public bool Estado { get; set; }
    public virtual ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();
}
