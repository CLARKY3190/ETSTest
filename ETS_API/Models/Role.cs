using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public virtual ICollection<Privilege> Privileges { get; set; } = new List<Privilege>();
}
