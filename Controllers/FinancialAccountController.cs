using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FinancesManager.Domain.Entities;
using FinancesManager.DataProvider.Contexts;
using FinancesManager.Models;
using Microsoft.AspNet.Identity;
using FinancesManager.Services.Interfaces;
using FinancesManager.Services.DTO;

namespace FinancesManager.Controllers
{
    [Authorize]
    public class FinancialAccountController : BaseApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        /*private Repositories.Repository<FinancialAccount> financialAccountRepository;
        private Repositories.Repository<AccountMember> accountMemberRepository;
        private Repositories.Repository<Transaction> transactionRepository;
        */
        private readonly IFinancialAccountBL financialAccountService;

        public FinancialAccountController(IFinancialAccountBL _service)
        {
            financialAccountService = _service;
        }



        public FinancialAccountController()
        {
            //financialAccountRepository = new Repositories.Repository<FinancialAccount>(db);
            //accountMemberRepository = new Repositories.Repository<AccountMember>(db);
            //transactionRepository = new Repositories.Repository<Transaction>(db);
        }

        // GET api/FinancialAccount
        public IQueryable<FinancialAccount> GetFinancialAccount()
        {
            return null;//db.FinancialAccount;
        }

        // GET api/FinancialAccount/5
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public IHttpActionResult GetFinancialAccount(long id)
        {
            var favm = financialAccountService.GetFinancialAccountViewModel(id, UserRecord);
            /*
            FinancialAccount financialaccount = db.FinancialAccount.Find(id);
            if (financialaccount == null)
            {
                return NotFound();
            }

            var accountMembers = accountMemberRepository.GetAll().FindAll(am => am.Account.Id == id);
            bool isMember = accountMembers.FindAll(am => am.User == UserRecord).Count == 1;
            if (!isMember)
            {
                return BadRequest("user not in account");
            }

            List<TransactionViewModel> transactions = transactionRepository.GetAll().FindAll(t => t.Account.Id == id)
                .Select(t => new TransactionViewModel {id = t.Id, amount = t.Amount, name = t.Name }).ToList();

            AccountMemberViewModel amvm = new AccountMemberViewModel { id = id, name = financialaccount.Name, owner = financialaccount.User == UserRecord };
            List<UserInFinAccountViewModel> users = accountMembers.Select(am => new UserInFinAccountViewModel{username = am.User.UserName}).ToList();
            FinancialAccountViewModel favm = new FinancialAccountViewModel { account = amvm, users = users, transactions = transactions };
            */
            return Ok(favm);
        }

        // PUT api/FinancialAccount/5
        public IHttpActionResult PutFinancialAccount(long id, FinancialAccountDTO financialaccount)
        {
            financialAccountService.Rename(id, financialaccount.name);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/FinancialAccount
        [ResponseType(typeof(FinancialAccountDTO))]
        public IHttpActionResult PostFinancialAccount(FinancialAccountDTO financialaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //financialaccount.User = UserRecord;
            financialAccountService.Create(financialaccount, UserRecord);
            //db.FinancialAccount.Add(financialaccount);
            //db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = 47 }, financialaccount);
        }

        // DELETE api/FinancialAccount/5
        [ResponseType(typeof(FinancialAccount))]
        public IHttpActionResult DeleteFinancialAccount(long id)
        {
            financialAccountService.Delete(id);
            /*
            FinancialAccount financialaccount = db.FinancialAccount.Find(id);
            if (financialaccount == null)
            {
                return NotFound();
            }

            db.FinancialAccount.Remove(financialaccount);
            db.SaveChanges();
            */
            return Ok();// Ok(financialaccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FinancialAccountExists(long id)
        {
            return true;//db.FinancialAccount.Count(e => e.Id == id) > 0;
        }
    }
}