using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;
public class DiscountContext:DbContext
{
    public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
    {
        
    }
    public DbSet<Coupon> Coupons { get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Description", Amount = 5 },
            new Coupon { Id = 2, ProductName = "SamSung 10", Description = "SamSung Description", Amount = 6 }
            );
    }
}
