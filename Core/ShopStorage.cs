using System;
using System.Collections.Generic;
using ExpressOrders.Core.Models;
using ExpressOrders.Persistense;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using System.Transactions;

namespace ExpressOrders.Core
{
    public class ShopStorage
    {
        public void Reserve(ProductDataModel product, int count)
        {
            using ExpressOrdersContext dbContext = new ExpressOrdersContext();
            using TransactionScope scope = new TransactionScope(
                TransactionScopeOption.RequiresNew, 
                new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.Serializable });
            
            Product entity = dbContext.Product.Where(prod => prod.Name.Equals(product.Name)).Single();

            if (entity.Stock == 0 || count > entity.Stock)
            {
                throw new ArgumentOutOfRangeException($"\nВыбранное количество товаров нет в наличии. Осталось: {entity.Stock}");
            }

            dbContext.Order.Add(new Order { ProductId = entity.Id, Count = count });
            entity.Stock -= count;
            dbContext.SaveChanges();
            scope.Complete();
        }

        public void Add(ProductDataModel product, int stock)
        {
            using ExpressOrdersContext dbContext = new ExpressOrdersContext();
            dbContext.Product.Add(new Product { Name = product.Name, Description = "Simple Description", Price = 12.02M, Stock = stock });
            dbContext.SaveChanges();
        }
    }
}
