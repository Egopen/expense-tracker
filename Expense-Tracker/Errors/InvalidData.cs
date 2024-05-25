namespace Expense_Tracker.Errors
{
    public class InvalidData:ArgumentException
    {
        public InvalidData() : base("Invalid password or email") { }
        
    }
}
