using System;
using System.Collections.Generic;

namespace ExpressOrders.Persistense
{
    public partial class Order
    {
        public long Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
