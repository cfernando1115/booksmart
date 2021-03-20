﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BookSmart.Models;
using BookSmart.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository;


namespace BookSmart.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {

        public BookRepository(ApplicationDbContext context)
            : base(context) { }

        public ApplicationDbContext ApplicationDbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task<IEnumerable<Book>> GetBooksWithGenresAsync() 
        {
            return await ApplicationDbContext.Books.Include(b => b.Genre).ToListAsync();
        }

        public async Task<Book> GetBookWithGenreAsync(int? id)
        {
            return await ApplicationDbContext.Books.Include(b => b.Genre).FirstOrDefaultAsync(b => b.Id == id);
        }
    }
}