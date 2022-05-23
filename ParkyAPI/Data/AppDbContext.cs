using Microsoft.EntityFrameworkCore;
using ParkyAPI.Models;

namespace ParkyAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        DbSet<NationalPark> NationalParks { get; set; }
    }
}
