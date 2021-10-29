using Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contracts
{
    public interface IAuthorRepository : IRepository<Author>
    {
        void Update(Author author);
    }
}
