public class ExpenseCTO
{
    public int ExpenseID { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }

    // Add other properties as needed

    // Constructor to populate the ExpenseDTO object
    public ExpenseCTO(int expenseID, string description, decimal amount)
    {
        ExpenseID = expenseID;
        Description = description;
        Amount = amount;
    }
}