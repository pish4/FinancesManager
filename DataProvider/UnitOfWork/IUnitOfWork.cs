using FinancesManager.DataProvider.Contexts;
using FinancesManager.Domain.Entities;
using FinancesManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancesManager.DataProvider.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<FinancialAccount> FinancialAccounts { get; }

        IRepository<Transaction> Transactions { get; }

        IRepository<AccountMember> AccountMembers { get; }

        IQueryable<ApplicationUser> ApplicationUsers { get; }

        void Save();

        void Dispose(bool disposing);
    }
}
