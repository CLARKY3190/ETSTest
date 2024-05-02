using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class PaymentMethod
{
    public int PaymentMethodId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? CardNumber { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual Etsuser? User { get; set; }
}
