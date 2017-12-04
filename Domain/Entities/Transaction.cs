using FinancesManager.DataProvider.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Domain.Entities
{
    public class Transaction: BaseEntity
    {
        public String Name { get; set; }
        public float Amount { get; set; }
        public ApplicationUser User { get; set; }
        public FinancialAccount Account { get; set; }
    }
}