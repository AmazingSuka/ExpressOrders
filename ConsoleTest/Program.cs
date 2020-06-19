using ExpressOrders.Core;
using ExpressOrders.Core.Models;
using ExpressOrders.Persistense;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleTest
{
    class Program
    {
        public static void PrepareDb()
        {
            using ExpressOrdersContext dbContext = new ExpressOrdersContext();
            if (!dbContext.Database.EnsureCreated())
            {
                dbContext.Database.ExecuteSqlRaw("DELETE FROM public.\"Order\"");
                dbContext.Database.ExecuteSqlRaw("DELETE FROM public.\"Product\"");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Подготовка.");
            PrepareDb();
            Console.WriteLine("Подготовка закончена.");
            ShopStorage storage = new ShopStorage();
            Random random = new Random();
            ProductDataModel product = new ProductDataModel("Car");
            storage.Add(product, 100);
            Console.WriteLine("Запуск потоков.");

            for (int k = 0; k < 10; k++)
            {
                Thread thread = new Thread(
                    new ThreadStart(() =>
                    {
                        ShopStorage threadStorage = new ShopStorage();
                                    Dictionary<int, List<bool>> result = new Dictionary<int, List<bool>>();

                        for (int i = 0; i < 1000; i++)
                        {
                            try
                            {
                                int randomCount = random.Next(1, 3);
                                threadStorage.Reserve(product, randomCount);
                            }
                            catch (ArgumentOutOfRangeException e)
                            {
                                Console.WriteLine(e.Message);
                                // break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    })
                );

                thread.Start();
                thread.Join();
            }

            Console.WriteLine("Completed.");
            Console.ReadLine();
        }
    }
}
