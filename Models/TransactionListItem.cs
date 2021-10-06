namespace GeneralStoreAPI.Models
{
    public class TransactionListItem
    {
        public int TransactionId { get; set; }
        // public int CustomerId { get; set; }
        // public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
    }
}