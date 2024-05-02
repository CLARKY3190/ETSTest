using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
