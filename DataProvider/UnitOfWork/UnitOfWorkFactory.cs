using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.DataProvider.UnitOfWork
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new UnitOfWork();
        }
    }
}