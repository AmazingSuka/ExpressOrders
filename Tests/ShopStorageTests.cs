using ExpressOrders.Core;
using ExpressOrders.Core.Models;
using ExpressOrders.Persistense;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ExpressOrders.Tests
{
    public class ShopStorageTests
    {
        [Fact]
        public void MainTest()
        {
            // Init
            ExpressOrdersContext dbContext = new ExpressOrdersContext();

            if (!dbContext.Database.EnsureCreated())
            {
                dbContext.Database.ExecuteSqlRaw("DELETE FROM public.\"Order\"");
                dbContext.Database.ExecuteSqlRaw("DELETE FROM public.\"Product\"");
            }

            ShopStorage storage = new ShopStorage();
            Random random = new Random();
            ProductDataModel product = new ProductDataModel("Car");
            int productsCount = 100;
            storage.Add(product, productsCount);

            // Act
            for (int k = 0; k < 10; k++)
            {
                Thread thread = new Thread(
                    new ThreadStart(() =>
                    {
                        ShopStorage threadStorage = new ShopStorage();
                        for (int i = 0; i < 1000; i++)
                        {
                            try
                            {
                                threadStorage.Reserve(product, random.Next(1, 3));
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                break;
                            }
                        }
                    })
                );

                thread.Start();
                thread.Join();
            }

            // Assert
            Assert.Equal(0, dbContext.Product.First().Stock);
            Assert.Equal(productsCount, dbContext.Order.Sum(o => o.Count));
        }
    }
}
