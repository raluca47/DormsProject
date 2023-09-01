using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Complex
{
    public int ComplexId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Dorm> Dorms { get; set; } = new List<Dorm>();
}
