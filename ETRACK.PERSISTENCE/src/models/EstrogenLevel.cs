using System;
using System.Collections.Generic;

namespace src.models;

public partial class EstrogenLevel
{
    public string LevelId { get; set; } = null!;

    public int UserId { get; set; }

    public DateOnly DateTested { get; set; }

    public int LevelPmol { get; set; }

    public int LevelPgml { get; set; }

    public virtual User User { get; set; } = null!;
}
