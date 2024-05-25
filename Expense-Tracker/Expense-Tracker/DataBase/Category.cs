using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.DataBase
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        List<Expense> Expenses { get; set; }
    }
}
