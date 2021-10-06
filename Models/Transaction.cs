using System;
using System.Collections.Generic;

#nullable disable

namespace GeneralStoreAPI.Models
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public DateTime DateOfTransaction { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
