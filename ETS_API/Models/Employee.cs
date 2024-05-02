using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int? CompanyId { get; set; }

    public string? Name { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<Etsuser> Etsusers { get; set; } = new List<Etsuser>();
}
