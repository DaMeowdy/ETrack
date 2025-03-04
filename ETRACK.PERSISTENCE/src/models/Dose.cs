using System;
using System.Collections.Generic;

namespace src.models;

public partial class Dose
{
    public string DoseId { get; set; } = null!;

    public int UserId { get; set; }

    public string DosageId { get; set; } = null!;

    public bool? IsDone { get; set; }

    public DateOnly DateScheduled { get; set; }

    public virtual Dosage Dosage { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
