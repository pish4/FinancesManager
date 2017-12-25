using FinancesManager.DataProvider.Contexts;
using FinancesManager.DataProvider.UnitOfWork;
using FinancesManager.Domain.Entities;
using FinancesManager.Services.DTO;
using FinancesManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Services.Implementation
{
    public class TransactionBL : BaseBL, ITransactionBL
    {
        public TransactionBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public void Create(TransactionDTO transaction, ApplicationUser user)
        {
            UseDb(uow =>
            {
                uow.Transactions.Create(new Transaction
                {
                    Name = transaction.name,
                    Amount = transaction.amount,
                    Account = uow.FinancialAccounts.GetById(transaction.account_id),
                    UserId = user.Id
                });
                uow.Save();
            });
        }
    }
}