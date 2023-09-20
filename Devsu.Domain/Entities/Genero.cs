using System;
using System.Collections.Generic;

namespace Devsu.Domain.Entities;

public partial class Genero : EntityBase<byte>
{
    //public byte Id { get; set; }

    public string Descripcion { get; set; } = null!;

    //public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
