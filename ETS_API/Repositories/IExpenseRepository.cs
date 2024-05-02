using System.Collections.Generic;
using System.Threading.Tasks;

public interface IExpenseRepository
{
    Task<ExpenseBO> GetExpenseByIdAsync(int id);
}