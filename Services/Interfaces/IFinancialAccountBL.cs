using FinancesManager.DataProvider.Contexts;
using FinancesManager.Domain.Entities;
using FinancesManager.Models;
using FinancesManager.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancesManager.Services.Interfaces
{
    public interface IFinancialAccountBL
    {
        void Create(FinancialAccountDTO financialaccount, ApplicationUser UserRecord);
        List<FinancialAccountDTO> GetAllByUser(ApplicationUser user);
        FinancialAccountViewModel GetFinancialAccountViewModel(long id, ApplicationUser user);
        void Rename(long id, string newName);
        void Delete(long id);
    }
}
