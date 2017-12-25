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
using FinancesManager.Services.Interfaces;
using Microsoft.AspNet.Identity;
using FinancesManager.Services.DTO;

namespace FinancesManager.Controllers
{
    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
    public class AccountMemberController : BaseApiController
    {

        private IAccountMemberBL accountMemberService;

        public AccountMemberController(IAccountMemberBL _service)
        {
            accountMemberService = _service;
        }

        // GET api/AccountMember
        public IQueryable<AccountMember> GetAccountMembers()
        {
            return null;//db.AccountMembers;
        }

        // GET api/AccountMember/5
        [ResponseType(typeof(AccountMember))]
        public IHttpActionResult GetAccountMember(long id)
        {
            /*
            AccountMember accountmember = db.AccountMembers.Find(id);
            if (accountmember == null)
            {
                return NotFound();
            }
            */
            return Ok();// (accountmember);
        }

        // PUT api/AccountMember/5
        public IHttpActionResult PutAccountMember(long id, AccountMember accountmember)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accountmember.Id)
            {
                return BadRequest();
            }

            db.Entry(accountmember).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountMemberExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            */
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/AccountMember
        public IHttpActionResult PostAccountMember(AccountMemberDTO accountMember)
        {
            accountMemberService.Create(accountMember, UserRecord);
            /*
            var accounts = financialAccountRepository.GetAll().FindAll(am => am.UserId == UserRecord && am.Id == accountId);
            if (accounts.Count != 1)
            {
                return BadRequest();
            }
            var financialAccount = accounts.First();

            var user = UserManager.FindByNameAsync(username).Result;
            if (user == null)
            {
                return BadRequest();
            }


            db.AccountMembers.Add(new AccountMember { Account = financialAccount, UserId = user});
            db.SaveChanges();
            */
            return Json(accountMember);
        }

        /*
        // POST api/AccountMember
        [ResponseType(typeof(AccountMember))]
        public IHttpActionResult PostAccountMember(AccountMember accountmember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccountMembers.Add(accountmember);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = accountmember.Id }, accountmember);
        }
         * */

        // DELETE api/AccountMember/5
        [ResponseType(typeof(AccountMember))]
        public IHttpActionResult DeleteAccountMember(long id)
        {
            /*
            AccountMember accountmember = db.AccountMembers.Find(id);
            if (accountmember == null)
            {
                return NotFound();
            }

            db.AccountMembers.Remove(accountmember);
            db.SaveChanges();
            */
            return Ok();// Ok(accountmember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountMemberExists(long id)
        {
            return true;// db.AccountMembers.Count(e => e.Id == id) > 0;
        }
    }
}