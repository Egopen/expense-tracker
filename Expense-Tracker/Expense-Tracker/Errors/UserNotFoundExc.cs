namespace Expense_Tracker.Errors
{
    public class UserNotFoundExc:ArgumentException
    {
        public UserNotFoundExc():base("User not found") 
        { }
    }
}
