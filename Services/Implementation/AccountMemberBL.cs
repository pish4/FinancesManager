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
    public class AccountMemberBL : BaseBL, IAccountMemberBL
    {
        public AccountMemberBL(IUnitOfWorkFactory factory)
            : base(factory)
        {
        }

        public void Create(AccountMemberDTO accountMember, ApplicationUser user)
        {
            UseDb(uow =>
            {
                uow.AccountMembers.Create(new AccountMember
                {
                    Account = uow.FinancialAccounts.GetById(accountMember.id),
                    UserId = uow.ApplicationUsers.First(u => u.UserName == accountMember.name).Id
                });
                uow.Save();
            });
        }

        public List<AccountMemberDTO> GetAllByUser(ApplicationUser user)
        {
            return UseDb(uow =>
            {
                return uow.AccountMembers.GetAll().FindAll(am => am.UserId == user.Id)
                    .Select(am => new AccountMemberDTO {
                        id = am.Account.Id,
                        name = am.Account.Name
                    }).ToList();
            });
        }
    }
}