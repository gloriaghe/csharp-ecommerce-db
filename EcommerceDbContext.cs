
using Microsoft.EntityFrameworkCore;

public class EcommerceDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employe> Employee { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=db-ecommerce;Integrated Security=True;Encrypt=False");
    }
}