using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public int? UserId { get; set; }

    public int? ExpenseCategoryId { get; set; }

    public decimal? Amount { get; set; }

    public string? Description { get; set; }

    public DateOnly? ExpenseDate { get; set; }

    public int? PaymentMethodId { get; set; }

    public virtual ExpenseCategory? ExpenseCategory { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual Etsuser? User { get; set; }
}
