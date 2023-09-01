using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Dorm
{
    public int DormId { get; set; }

    public int AdressId { get; set; }

    public int ComplexId { get; set; }

    public virtual ICollection<Administrator> Administrators { get; set; } = new List<Administrator>();

    public virtual Address Adress { get; set; } = null!;

    public virtual Complex Complex { get; set; } = null!;

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
