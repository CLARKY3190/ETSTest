using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class Etsuser
{
    public int UserId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual UserRole? UserRole { get; set; }
}
