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
    public class ConstantRepository : Repository<Constant>, IConstantRepository
    {
        private readonly ApplicationDbContext _db;

        public ConstantRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _db = dbContext;
        }

        public void Update(Constant constant)
        {
            var dbConstant = _db.Constants.FirstOrDefault(x => x.Key == constant.Key);
            if (dbConstant is null)
                throw new Exception("Constante no encontrada");

            var constantJson = JsonConvert.SerializeObject(constant);
            JsonConvert.PopulateObject(constantJson, dbConstant);
            _db.SaveChanges();
        }
    }
}
