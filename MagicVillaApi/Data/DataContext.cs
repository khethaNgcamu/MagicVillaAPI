using MagicVillaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Villa> Villas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
           new Villa()
           {
               Id = 1,
               Name = "Royal villa",
               Details = "this a royal villa reserved for a royal family",
               ImageUrl = "",
               Occupancy = 5,
               Rate = 200,
               Sqft = 500,
               Amenity = "",
               CreatedDate = DateTime.Now
           });
        }
    }
   

}
