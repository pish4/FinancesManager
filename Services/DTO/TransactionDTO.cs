using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinancesManager.Services.DTO
{
    public class TransactionDTO
    {
        public string name { get; set; }
        public float amount { get; set; }
        public long account_id { get; set; }
    }
}