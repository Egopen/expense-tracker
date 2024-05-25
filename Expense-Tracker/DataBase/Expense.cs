using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.DataBase
{
    public class Expense
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double Sum {  get; set; }
        public string Description { get; set; }
        [Required]
        public DateOnly Date {  get; set; }

        [ForeignKey("UserId")]
        public string UserId {  get; set; }
        [Required]

        public User User { get; set; }
        public int CategoryId {  get; set; }
        [Required]
        public Category Category { get; set; }

    }
}
