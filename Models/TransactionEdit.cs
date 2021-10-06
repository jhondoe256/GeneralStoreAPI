using System;

namespace GeneralStoreAPI.Models
{
    public class TransactionEdit
    {
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public int Quantity { get; set; }   
        public decimal TotalCost {get;set;}  
    }
}