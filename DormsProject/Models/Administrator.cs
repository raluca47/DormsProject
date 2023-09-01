using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Administrator
{
    public int AdministratorId { get; set; }

    public string Cnp { get; set; } = null!;

    public int DormId { get; set; }

    public virtual Person CnpNavigation { get; set; } = null!;

    public virtual Dorm Dorm { get; set; } = null!;
}
