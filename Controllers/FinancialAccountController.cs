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

namespace FinancesManager.Controllers
{
    [Authorize]
    public class FinancialAccountController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Repositories.Repository<FinancialAccount> financialAccountRepository;
        private Repositories.Repository<AccountMember> accountMemberRepository;
        private Repositories.Repository<Transaction> transactionRepository;

        // GET api/FinancialAccount
        public IQueryable<FinancialAccount> GetFinancialAccount()
        {
            return db.FinancialAccount;
        }

        // GET api/FinancialAccount/5
        public IHttpActionResult GetFinancialAccount(long id)
        {
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

            return Ok(favm);
        }

        // PUT api/FinancialAccount/5
        public IHttpActionResult PutFinancialAccount(long id, FinancialAccount financialaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != financialaccount.Id)
            {
                return BadRequest();
            }

            db.Entry(financialaccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/FinancialAccount
        [ResponseType(typeof(FinancialAccount))]
        public IHttpActionResult PostFinancialAccount(FinancialAccount financialaccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            financialaccount.User = UserRecord;
            db.FinancialAccount.Add(financialaccount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = financialaccount.Id }, financialaccount);
        }

        // DELETE api/FinancialAccount/5
        [ResponseType(typeof(FinancialAccount))]
        public IHttpActionResult DeleteFinancialAccount(long id)
        {
            FinancialAccount financialaccount = db.FinancialAccount.Find(id);
            if (financialaccount == null)
            {
                return NotFound();
            }

            db.FinancialAccount.Remove(financialaccount);
            db.SaveChanges();

            return Ok(financialaccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FinancialAccountExists(long id)
        {
            return db.FinancialAccount.Count(e => e.Id == id) > 0;
        }
    }
}