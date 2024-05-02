using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class UserRole
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Etsuser User { get; set; } = null!;
}
