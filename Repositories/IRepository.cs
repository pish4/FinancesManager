using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancesManager.Repositories
{
    public interface IRepository<TEntity>
    {
        List<TEntity> GetAll();

        TEntity GetById(object id);

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(object id);

        IQueryable<TEntity> Query();
    }
}
