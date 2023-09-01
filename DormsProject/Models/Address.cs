using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string Street { get; set; } = null!;

    public int? Number { get; set; }

    public virtual ICollection<Dorm> Dorms { get; set; } = new List<Dorm>();

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
