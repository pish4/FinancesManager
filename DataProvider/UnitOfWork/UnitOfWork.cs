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
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private IRepository<FinancialAccount> financialAccountRepository;
        private IRepository<Transaction> transactionRepository;
        private IRepository<AccountMember> accountMemberRepository;

        public IRepository<FinancialAccount> FinancialAccounts
        {
            get
            {
                if (financialAccountRepository == null)
                {
                    financialAccountRepository = new Repository<FinancialAccount>(db);
                }

                return financialAccountRepository;
            }
        }

        public IRepository<Transaction> Transactions
        {
            get
            {
                if (transactionRepository == null)
                {
                    transactionRepository = new Repository<Transaction>(db);
                }

                return transactionRepository;
            }
        }

        public IRepository<AccountMember> AccountMembers
        {
            get
            {
                if (accountMemberRepository == null)
                {
                    accountMemberRepository = new Repository<AccountMember>(db);
                }

                return accountMemberRepository;
            }
        }

        public IQueryable<ApplicationUser> ApplicationUsers
        {
            
            get
            {
                return db.Users.AsQueryable();
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
