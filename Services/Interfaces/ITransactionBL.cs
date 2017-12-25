using FinancesManager.DataProvider.Contexts;
using FinancesManager.Domain.Entities;
using FinancesManager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Services.Interfaces
{
    public interface ITransactionBL// : IBaseBL<Transaction>
    {
        void Create(TransactionDTO transaction, ApplicationUser user);
    }
}