using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.RequestJson
{
    public class RegistrationRequest
    {
        [Required]
        public string Email { get; set; }
        [Required] 
        public string Username { get; set;}
        [Required]
        public string Password { get; set; }
    }
}
