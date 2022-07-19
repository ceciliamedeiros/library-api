using first_api.Models;
using Microsoft.EntityFrameworkCore;

namespace first_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; } 
    }
}
