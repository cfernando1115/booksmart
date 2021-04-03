using BookSmart.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookSmart.Models
{
    public class SeedData
    {
        public static void InitializeMembershipTypes(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            if (context.MembershipTypes.Any())
            {
                return;
            }

            context.MembershipTypes.AddRange(
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
            context.SaveChanges();
        }

        public static void InitializeGenres(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
            if (context.Genres.Any())
            {
                return;
            }

            context.Genres.AddRange(
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
            context.SaveChanges();
        }
    }
}
