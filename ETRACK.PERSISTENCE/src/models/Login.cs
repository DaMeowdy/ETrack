using System;
using System.Collections.Generic;

namespace src.models;

public partial class Login
{
    public string LoginId { get; set; } = null!;

    public int? UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public virtual User? User { get; set; }
}
