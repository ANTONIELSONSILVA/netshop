using Microsoft.EntityFrameworkCore;
using netshop.ProductAPI.models;
namespace netshop.ProductAPI.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> products { get; set; }

    //fluent API
    // alterando convenções do Entity framework
    protected override void OnModelCreating(ModelBuilder mB)
    {
        // Caterory
        mB.Entity<Category>().HasKey(c => c.CategoryId);
        mB.Entity<Category>().Property(c => c.Name).HasMaxLength(100).
            IsRequired();

        // Product
        mB.Entity<Product>().Property(c => c.Name).HasMaxLength(100).
            IsRequired();

        mB.Entity<Product>().Property(c => c.Description).HasMaxLength(255).
            IsRequired();

        mB.Entity<Product>().Property(c => c.ImageURL).HasMaxLength(255).
            IsRequired();

        mB.Entity<Product>().Property(c => c.Price).HasPrecision(12, 2);

        mB.Entity<Category>().HasMany(g => g.Products)
            .WithOne(c => c.Category)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // popular 
        mB.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Material Escolar",
            },
            new Category
            {
                CategoryId = 2,
                Name = "Acessórios",
            }
        );

    }

}
