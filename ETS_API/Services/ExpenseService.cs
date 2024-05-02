using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
    }
    public async Task<ExpenseBO> GetExpenseByIdAsync(int id)
    {
        var expense = await _expenseRepository.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return null; // Return null or handle the scenario when no expense is found
        }

        return new ExpenseBO
        {
            ExpenseID = expense.ExpenseID,
            Amount = expense.Amount,
            Description = expense.Description        
        };
    }
}