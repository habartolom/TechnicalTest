using Microsoft.EntityFrameworkCore;

using Persistence.AccessData;
using Persistence.Contracts;
using Persistence.Repositories;
using System;

namespace Persistence.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ApplicationDbContext _db;
        private bool disposed = false;
        private IAuthorRepository authors;
        private IBookRepository books;
        private IConstantRepository constants;

        public RepositoryFactory(ApplicationDbContext db)
        {
            _db = db;
        }

        public IAuthorRepository Authors
        {
            get
            {
                if (authors == null)
                    authors = new AuthorRepository(_db);
                return authors;
            }
        }

        public IBookRepository Books
        {
            get
            {
                if (books == null)
                    books = new BookRepository(_db);
                return books;
            }
        }

        public IConstantRepository Constants
        {
            get
            {
                if (constants == null)
                    constants = new ConstantRepository(_db);
                return constants;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                    _db.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Commit()
        {
            return _db.SaveChanges();
        }

    }
}
