namespace Expense_Tracker.ResponseJson
{
    public class GraficalInfoRes
    {
        public byte[] Image { get; set; }
        public List<CategoryExpenseInfoRes> CategoryInfo { get; set; }
        public GraficalInfoRes() { 
            CategoryInfo = new List<CategoryExpenseInfoRes>();
        }

    }
}
