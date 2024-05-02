using System.Collections.Generic;
using System.Threading.Tasks;

public interface IExpenseService
{
    Task<ExpenseBO> GetExpenseByIdAsync(int id);
}