using System;
using System.Collections.Generic;

namespace src.models;

public partial class Dosage
{
    public string DosageId { get; set; } = null!;

    public int UserId { get; set; }

    public string Concentration { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Ester { get; set; } = null!;

    public virtual ICollection<Dose> Doses { get; set; } = new List<Dose>();

    public virtual User User { get; set; } = null!;
}
