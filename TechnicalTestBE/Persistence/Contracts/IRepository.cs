using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void DeleteById(object id);
        
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = null);
        
        public TEntity GetById(object id);
        
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null);
        
        public TEntity Insert(TEntity entity);
    }
}
