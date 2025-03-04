using System;
using System.Collections.Generic;

namespace src.models;

public partial class User
{
    public int UserId { get; set; }

    public string PreferredMeasurement { get; set; } = null!;

    public virtual ICollection<Dosage> Dosages { get; set; } = new List<Dosage>();

    public virtual ICollection<Dose> Doses { get; set; } = new List<Dose>();

    public virtual ICollection<EstrogenLevel> EstrogenLevels { get; set; } = new List<EstrogenLevel>();

    public virtual Login? Login { get; set; }
}
