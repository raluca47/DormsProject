using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Faculty
{
    public char Building { get; set; }

    public string DomainName { get; set; } = null!;

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();

    public virtual ICollection<Professor> ProfessorsNavigation { get; set; } = new List<Professor>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
