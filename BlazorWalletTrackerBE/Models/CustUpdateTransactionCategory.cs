namespace BlazorWalletTrackerBE.Models
{
    public class CustUpdateTransactionCategory
    {
        public int UserId { get; set; }
        public string OldCategory { get; set; }
        public string NewCategory { get; set; }
    }
}
