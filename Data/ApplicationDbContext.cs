using Microsoft.EntityFrameworkCore;
using BookSmart.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookSmart.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
