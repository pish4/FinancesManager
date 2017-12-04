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

namespace FinancesManager.Controllers
{
    public class AccountMemberController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET api/AccountMember
        public IQueryable<AccountMember> GetAccountMembers()
        {
            return db.AccountMembers;
        }

        // GET api/AccountMember/5
        [ResponseType(typeof(AccountMember))]
        public IHttpActionResult GetAccountMember(long id)
        {
            AccountMember accountmember = db.AccountMembers.Find(id);
            if (accountmember == null)
            {
                return NotFound();
            }

            return Ok(accountmember);
        }

        // PUT api/AccountMember/5
        public IHttpActionResult PutAccountMember(long id, AccountMember accountmember)
        {
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

            return StatusCode(HttpStatusCode.NoContent);
        }

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

        // DELETE api/AccountMember/5
        [ResponseType(typeof(AccountMember))]
        public IHttpActionResult DeleteAccountMember(long id)
        {
            AccountMember accountmember = db.AccountMembers.Find(id);
            if (accountmember == null)
            {
                return NotFound();
            }

            db.AccountMembers.Remove(accountmember);
            db.SaveChanges();

            return Ok(accountmember);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccountMemberExists(long id)
        {
            return db.AccountMembers.Count(e => e.Id == id) > 0;
        }
    }
}