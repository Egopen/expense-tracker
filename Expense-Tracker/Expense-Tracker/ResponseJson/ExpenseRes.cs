using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.ResponseJson
{
    public class ExpenseRes
    {
        
        public int Id { get; set; }
        public double Sum { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; } 
    }
}
