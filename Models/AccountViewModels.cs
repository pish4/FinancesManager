using FinancesManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FinancesManager.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class UserHomeInfoModel
    {
         public List<FinancialAccount> accounts { get; set; }
         public List<AccountMember> shared_accounts { get; set; }
    }

    public class TransactionViewModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public float amount { get; set; }
    }

    public class AccountMemberViewModel
    {
        public long id { get; set; }
        public string name { get; set; }
        public bool owner { get; set; }
    }

    public class UserInFinAccountViewModel {
        public string username {get; set;}
    }

    public class FinancialAccountViewModel
    {
        public AccountMemberViewModel account {get;set;}
        public List<UserInFinAccountViewModel> users { get; set; }
        public List<TransactionViewModel> transactions { get; set; }
    }
}
