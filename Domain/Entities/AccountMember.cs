using FinancesManager.DataProvider.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Domain.Entities
{
    public class AccountMember : BaseEntity
    {
        public string UserId { get; set; }
        public virtual FinancialAccount Account { get; set; }
    }
}