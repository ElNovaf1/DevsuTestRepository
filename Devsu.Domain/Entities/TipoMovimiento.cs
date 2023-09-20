using System;
using System.Collections.Generic;

namespace Devsu.Domain.Entities;

public partial class TipoMovimiento : EntityBase<byte>
{
    //public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
