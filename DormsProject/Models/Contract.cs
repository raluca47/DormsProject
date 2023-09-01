using System;
using System.Collections.Generic;

namespace DormsProject.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int StudentId { get; set; }

    public int RoomNumber { get; set; }

    public virtual Room RoomNumberNavigation { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
