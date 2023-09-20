using System;
using System.Collections.Generic;

namespace Devsu.Domain.Entities;

public partial class TipoCuenta : EntityBase<byte>
{
    //public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
