using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Professor
{
    public int ProfessorId { get; set; }

    public string Cnp { get; set; } = null!;

    public char Building { get; set; }

    public int RoomNumber { get; set; }

    public virtual Faculty BuildingNavigation { get; set; } = null!;

    public virtual Person CnpNavigation { get; set; } = null!;

    public virtual Room RoomNumberNavigation { get; set; } = null!;

    public virtual ICollection<Faculty> Buildings { get; set; } = new List<Faculty>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
