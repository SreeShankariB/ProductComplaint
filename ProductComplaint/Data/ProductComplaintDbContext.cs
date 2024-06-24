using Microsoft.EntityFrameworkCore;
using ProductComplaint.Models.Entities;

namespace ProductComplaint.Data
{
    public class ProductComplaintDbContext : DbContext
    {
        public ProductComplaintDbContext(DbContextOptions<ProductComplaintDbContext> options) : base(options)
        {
            
        }
        public DbSet<ProductComp> Products { get; set; }
    }
}
