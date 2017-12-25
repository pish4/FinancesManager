using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Services.DTO
{
    public class FinancialAccountDTO
    {
        public long id { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
    }
}