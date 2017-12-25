using FinancesManager.DataProvider.Contexts;
using FinancesManager.DataProvider.UnitOfWork;
using FinancesManager.Domain.Entities;
using FinancesManager.Models;
using FinancesManager.Services.DTO;
using FinancesManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FinancesManager.Services.Implementation
{
    public class FinancialAccountBL : BaseBL, IFinancialAccountBL
    {
        public FinancialAccountBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public FinancialAccountViewModel GetFinancialAccountViewModel(long id, ApplicationUser user)
        {
            
            return UseDb(uow =>
            {
                FinancialAccount acc = uow.FinancialAccounts.GetById(id);
                int sz = uow.Transactions.GetAll().Count;
                
                List<TransactionViewModel> transactionsVM = uow.Transactions.GetAll()
                    .FindAll(t => t.Account.Id == id).Select(t => new TransactionViewModel
                    {
                        id = t.Id,
                        amount = t.Amount,
                        name = t.Name
                    })
                    .ToList();

                List<UserInFinAccountViewModel> usersVM = uow.AccountMembers.GetAll()
                    .FindAll(am => am.Account.Id == id)
                    .Select(am => new UserInFinAccountViewModel
                    {
                        username = uow.ApplicationUsers.Where(au => au.Id == am.UserId).First().UserName
                    })
                    .ToList();

                return new FinancialAccountViewModel
                {    
                    account = new AccountMemberViewModel
                    {
                        id = id,
                        name = acc.Name,
                        owner = acc.UserId == user.Id
                    },
                    transactions = transactionsVM,
                    users = usersVM
                };
            });

        }

        public List<FinancialAccountDTO> GetAllByUser(ApplicationUser user) 
        {
            return UseDb(uow =>
            {
                return uow.FinancialAccounts.GetAll().FindAll(fa => fa.UserId == user.Id)
                    .Select(fa => new FinancialAccountDTO { 
                        id = fa.Id,
                        name = fa.Name,
                        owner = user.UserName
                    }).ToList();
            });
        }

        public void Create(FinancialAccountDTO account, ApplicationUser user)
        {
            UseDb(uow =>
            {
                uow.FinancialAccounts.Create(new FinancialAccount { 
                    Name = account.name,
                    UserId = user.Id
                });
                uow.Save();
            });
        }

        public void Rename(long id, string newName)
        {
            UseDb(uow =>
            {
                var acc = uow.FinancialAccounts.GetById(id);
                acc.Name = newName;
                uow.FinancialAccounts.Update(acc);
                uow.Save();
            });
        }

        public void Delete(long id)
        {
            UseDb(uow =>
            {
                uow.FinancialAccounts.Delete(id);
                uow.Save();
            });
        }
    }
}