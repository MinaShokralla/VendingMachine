using Microsoft.EntityFrameworkCore;
using VendingMachine.Models;

namespace VendingMachine.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<CustomerProduct> CustomerProducts => Set<CustomerProduct>();
        public DbSet<Deposit> UserDeposits => Set<Deposit>();
        public DbSet<Coin> Coins => Set<Coin>();




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}