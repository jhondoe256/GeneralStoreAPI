using System;
using System.Collections.Generic;

namespace GeneralStoreAPI.Models
{
    public class CustomerListItem
    {
        public int CustomerId { get; set; }
        public List<Transaction> Transactions { get; set; }=new List<Transaction>();
       
    }
}