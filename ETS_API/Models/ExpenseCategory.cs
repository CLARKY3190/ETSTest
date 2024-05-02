using System;
using System.Collections.Generic;

namespace ETS_API.Models;

public partial class ExpenseCategory
{
    public int ExpenseCategoryId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
