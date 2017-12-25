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
using FinancesManager.Services.DTO;
using Microsoft.AspNet.Identity;

namespace FinancesManager.Controllers
{
    [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
    public class TransactionController : BaseApiController
    {
        private ITransactionBL transactionService;

        public TransactionController(ITransactionBL _service)
        {
            transactionService = _service;
        }

        // GET api/Transaction
        public IQueryable<Transaction> GetTransactions()
        {
            return null;// db.Transactions;
        }

        // GET api/Transaction/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult GetTransaction(long id)
        {
            Transaction transaction = null;// = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // PUT api/Transaction/5
        public IHttpActionResult PutTransaction(long id, Transaction transaction)
        {
            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST api/Transaction
        [ResponseType(typeof(TransactionDTO))]
        public IHttpActionResult PostTransaction(TransactionDTO transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            transactionService.Create(transaction, UserRecord);
            return Json(transaction);
        }

        // DELETE api/Transaction/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult DeleteTransaction(long id)
        {
            /*
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            db.SaveChanges();
            */
            return Ok();// Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(long id)
        {
            return true; // db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}