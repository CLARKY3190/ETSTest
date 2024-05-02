using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETS_API.Models;
using Microsoft.EntityFrameworkCore;

public class ExpenseRepository : IExpenseRepository
{
    private readonly EnterpriseTrackingSystemContext _context;

    public ExpenseRepository(EnterpriseTrackingSystemContext context)
    {
        _context = context;
    }

    public async Task<ExpenseBO> GetExpenseByIdAsync(int id)
    {
        // Retrieve expense by ID from the database and map it to a BO
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            return null; // Return null if no expense is found
        }
        return MapToBO(expense); // Map and return the found expense
    }

    private ExpenseBO MapToBO(Expense entity)
    {
        // Implement mapping logic from entity to BO
        return new ExpenseBO
        {
            ExpenseID = entity.ExpenseId,
            Description = entity.Description,
            Amount = entity.Amount
            // Map other properties as needed
        };
    }
}