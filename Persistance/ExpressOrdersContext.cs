using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExpressOrders.Persistense
{
    public partial class ExpressOrdersContext : DbContext
    {
        public ExpressOrdersContext()
        {
        }

        public ExpressOrdersContext(DbContextOptions<ExpressOrdersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=ExpressOrders;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ExpressOrdersContext).Assembly);
        }
    }
}
