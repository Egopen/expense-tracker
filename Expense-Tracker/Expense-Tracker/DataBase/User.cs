using Microsoft.AspNetCore.Identity;

namespace Expense_Tracker.DataBase
{
    public class User:IdentityUser
    {
        public List<Expense> Expenses { get; set; } = new();
    }
}
