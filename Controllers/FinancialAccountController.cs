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
    [Authorize]
    public class FinancialAccountController : BaseApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET api/FinancialAccount
        public IQueryable<FinancialAccount> GetFinancialAccount()
        {
            return db.FinancialAccount;
        }

        // GET api/FinancialAccount/5
        [ResponseType(typeof(FinancialAccount))]
        public IHttpActionResult GetFinancialAccount(long id)
        {
            FinancialAccount financialaccount = db.FinancialAccount.Find(id);
            if (financialaccount == null)
            {
                return NotFound();
            }

            return Ok(financialaccount);
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