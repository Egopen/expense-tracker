namespace Expense_Tracker.RequestJson
{
    public class ExpenseReq
    {
        public double Sum { get; set; }
        public string Description { get; set; }
        public int Day {  get; set; }
        public int Month {  get; set; }
        public int Year { get; set; }
        public int CategoryId { get; set; }
    }
}
