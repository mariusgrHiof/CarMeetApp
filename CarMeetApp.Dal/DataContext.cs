using CarMeetApp.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarMeetApp.Dal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}