using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Room
{
    public int RoomNumber { get; set; }

    public int Floor { get; set; }

    public int DormId { get; set; }

    public bool Isoccupied { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Dorm Dorm { get; set; } = null!;

    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
