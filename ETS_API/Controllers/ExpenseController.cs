using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService ?? throw new ArgumentNullException(nameof(expenseService));
        }

        // Other action methods

        [HttpGet("expense/{expenseId}")]
        public async Task<ActionResult<ExpenseCTO>> GetExpenseById(int expenseId)
        {
            try
            {
                var expense = await _expenseService.GetExpenseByIdAsync(expenseId);
                if (expense == null)
                {
                    return NotFound($"Expense with ID {expenseId} not found");
                }
                return Ok(expense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
