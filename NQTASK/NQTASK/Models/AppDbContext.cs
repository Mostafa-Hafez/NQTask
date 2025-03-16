using Microsoft.EntityFrameworkCore;

namespace NQTASK.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice_Product>().HasKey(x => new { x.Pr_ID, x.In_ID });
            modelBuilder.Entity<Client>().HasIndex(x =>x.Code).IsUnique();
            modelBuilder.Entity<Account>().HasIndex(x =>x.UserName).IsUnique();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Invoice_Product> Invoice_Product { get; set; }


    }
}
