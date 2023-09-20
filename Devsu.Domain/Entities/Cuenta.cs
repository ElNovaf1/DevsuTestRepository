using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Devsu.Domain.Entities;

public partial class Cuenta : EntityBase<int>
{

    [Required(ErrorMessage = "Numero de Cuenta es requerido")]
    [StringLength(20)]
    public string Numero { get; set; } = null!;
    
    [Required(ErrorMessage = "Tipo de Cuenta es requerido")]
    [StringLength(20)]
    public string TipoCuenta { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cliente es requerido")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Saldo inicial es requerido")]
    public decimal SaldoInicial { get; set; }

    [Required(ErrorMessage = "estado de cuenta es requerido")]
    public bool Estado { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
