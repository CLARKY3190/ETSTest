using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class Privilege
{
    public int PrivilegeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
