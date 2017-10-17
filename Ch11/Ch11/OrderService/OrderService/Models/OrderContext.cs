using System;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Models
{
    public class OrderContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Orders.db");
        }
    }
}
