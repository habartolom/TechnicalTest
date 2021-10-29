using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using Persistence.AccessData;
using Persistence.Contracts;
using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Book book)
        {
            var dbBook = _db.Books.FirstOrDefault(x => x.BookId == book.BookId);
            if (dbBook is null)
                throw new Exception("Libro no encontrado");

            var bookJson = JsonConvert.SerializeObject(book);
            JsonConvert.PopulateObject(bookJson, dbBook);
            _db.SaveChanges();
        }
    }
}
