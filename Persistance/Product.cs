using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpressOrders.Persistense
{
    public partial class Product
    {
        public Product()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [ConcurrencyCheck]
        public int Stock { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
