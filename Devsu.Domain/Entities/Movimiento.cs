using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Devsu.Domain.Entities;

public partial class Movimiento : EntityBase<long>
{
    [Required(ErrorMessage = "Id de Cuenta es requerido")]
    public int CuentaId { get; set; }

    [Required(ErrorMessage = "Tipo de Movimiento es requerido")]
    [StringLength(20)]
    public string TipoMovimiento { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fecha de movimiento es requerido")]
    public DateTime Fecha { get; set; }

    [Required(ErrorMessage = "Valor es requerido")]
    public decimal Valor { get; set; }
    [Required(ErrorMessage = "Saldo es requerido")]
    public decimal Saldo { get; set; }

    public virtual Cuenta Cuenta { get; set; } = null!;

}
