using BookSmart.Data;
using BookSmart.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookSmart.SeedData
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<AppRole> _roleManager;

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception) { }

            if(_context.Roles.Any(r => r.Name == Utility.RoleHelper.Admin))
            {
                return;
            }

            _roleManager.CreateAsync(new AppRole { Name = Utility.RoleHelper.Admin }).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new AppRole { Name = Utility.RoleHelper.Member }).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Name = "Admin"
            }, Environment.Env.Password).GetAwaiter().GetResult();

            ApplicationUser user = _context.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            _userManager.AddToRoleAsync(user, Utility.RoleHelper.Admin).GetAwaiter().GetResult();

            _context.MembershipTypes.AddRange(
                new MembershipType
                {
                    Name = "I Don't Like Discounts",
                    BooksPerYear = 0,
                    DiscountPercentage = 0
                },
                new MembershipType
                {
                    Name = "The Occasional",
                    BooksPerYear = 12,
                    DiscountPercentage = 10,
                },
                new MembershipType
                {
                    Name = "The Avid",
                    BooksPerYear = 24,
                    DiscountPercentage = 20,
                },
                new MembershipType
                {
                    Name = "The Pandemic",
                    BooksPerYear = 36,
                    DiscountPercentage = 40,
                }
            );

            _context.Genres.AddRange(
                new Genre
                {
                    Name = "History"
                },
                new Genre
                {
                    Name = "Literature"
                },
                new Genre
                {
                    Name = "True Crime"
                },
                new Genre
                {
                    Name = "Biography"
                },
                new Genre
                {
                    Name = "Kids"
                },
                new Genre
                {
                    Name = "Romance"
                },
                new Genre
                {
                    Name = "Travel"
                },
                new Genre
                {
                    Name = "Comic"
                },
                new Genre
                {
                    Name = "Science Fiction"
                },
                new Genre
                {
                    Name = "Mystery"
                },
                new Genre
                {
                    Name = "Young Adult"
                },
                new Genre
                {
                    Name = "Fantasy"
                },
                new Genre
                {
                    Name = "Thriller"
                }
            );

            _context.Books.AddRange(
                new Book
                {
                    Name = "Book1",
                    Author = "Author1",
                    Price = 10.99f,
                    GenreId = 1
                },
                new Book
                {
                    Name = "Book2",
                    Author = "Author2",
                    Price = 10.99f,
                    GenreId = 2
                },
                new Book
                {
                    Name = "Book3",
                    Author = "Author3",
                    Price = 8.99f,
                    GenreId = 3
                },
                new Book
                {
                    Name = "Book4",
                    Author = "Author4",
                    Price = 8.99f,
                    GenreId = 2
                },
                new Book
                {
                    Name = "Book5",
                    Author = "Author5",
                    Price = 12.99f,
                    GenreId = 6
                },
                new Book
                {
                    Name = "Book6",
                    Author = "Author6",
                    Price = 11.99f,
                    GenreId = 3
                },
                new Book
                {
                    Name = "Book7",
                    Author = "Author7",
                    Price = 14.99f,
                    GenreId = 5
                },
                new Book
                {
                    Name = "Book8",
                    Author = "Author8",
                    Price = 10.99f,
                    GenreId = 9
                },
                new Book
                {
                    Name = "Book9",
                    Author = "Author9",
                    Price = 9.99f,
                    GenreId = 11
                },
                new Book
                {
                    Name = "Book10",
                    Author = "Author10",
                    Price = 8.99f,
                    GenreId = 4
                },
                new Book
                {
                    Name = "Book11",
                    Author = "Author11",
                    Price = 15.99f,
                    GenreId = 12
                },
                new Book
                {
                    Name = "Book12",
                    Author = "Author12",
                    Price = 14.99f,
                    GenreId = 13
                }
            );
                _context.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
