using System.Data.Entity;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.DAL
{
    public class OrderManagementContext : DbContext
    {
        public OrderManagementContext() : base("name=OrderManagementDB")
        {
            // Disable database initialization
            Database.SetInitializer<OrderManagementContext>(null);
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map entities to their actual table names
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Agent>().ToTable("Agent");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}