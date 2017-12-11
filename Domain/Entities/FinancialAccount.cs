using FinancesManager.DataProvider.Contexts;
using FinancesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Domain.Entities
{
    public class FinancialAccount: BaseEntity
    {
        public String Name { get; set; }

        public ApplicationUser User { get; set; }
    }
}