using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public int? StudyYear { get; set; }

    public string? FormOfEducation { get; set; }

    public string? Cnp { get; set; }

    public int RoomNumber { get; set; }

    public virtual Person? CnpNavigation { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Room RoomNumberNavigation { get; set; } = null!;

    public virtual ICollection<Faculty> Buildings { get; set; } = new List<Faculty>();

    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();
}
