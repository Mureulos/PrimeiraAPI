using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PrimeiraAPI.Models;

namespace PrimeiraAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base (options)
        {   
        }

        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<BookModel> Books { get; set; }
    }
}
