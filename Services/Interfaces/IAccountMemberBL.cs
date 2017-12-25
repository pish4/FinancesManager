using FinancesManager.DataProvider.Contexts;
using FinancesManager.Domain.Entities;
using FinancesManager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancesManager.Services.Interfaces
{
    public interface IAccountMemberBL// : IBaseBL<AccountMember>
    {
        void Create(AccountMemberDTO accountMember, ApplicationUser user);
        List<AccountMemberDTO> GetAllByUser(ApplicationUser user);
    }
}
