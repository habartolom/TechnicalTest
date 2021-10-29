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
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Author author)
        {
            var dbAuthor = _db.Authors.FirstOrDefault(x => x.AuthorId == author.AuthorId);
            if (dbAuthor is null)
                throw new Exception("Autor no encontrado");
            
            var authorJson = JsonConvert.SerializeObject(author);
            JsonConvert.PopulateObject(authorJson, dbAuthor);
            _db.SaveChanges();
        }
    }
}
