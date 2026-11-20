namespace BillingSystemAPI.Models
{
    public class BillingRecord
    {
        public int CustomerID { get; set; }
        public string BillingMonth { get; set; } = string.Empty;
        public int PreviousReading { get; set; }
        public int PresentReading { get; set; }
        public int Consumption { get; set; }
        public decimal RatePerCubic { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Unpaid";
    }
}