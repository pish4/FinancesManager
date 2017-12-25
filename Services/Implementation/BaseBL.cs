using FinancesManager.DataProvider.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Services.Implementation
{
    public abstract class BaseBL
    {
        private IUnitOfWorkFactory factory;

        public BaseBL(IUnitOfWorkFactory factory)
        {
            this.factory = factory;
        }

        protected TEntity UseDb<TEntity>(Func<IUnitOfWork, TEntity> func)
        {
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                return func(unitOfWork);
            }
        }

        protected void UseDb(Action<IUnitOfWork> func)
        {
            using (IUnitOfWork unitOfWork = factory.Create())
            {
                func(unitOfWork);
            }
        }
    }
}