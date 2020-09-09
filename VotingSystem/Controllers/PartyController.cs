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
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    public class PartyController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Party
        public IQueryable<Party> GetParties()
        {
            return db.Parties;
        }

        // GET: api/Party/5
        [ResponseType(typeof(Party))]
        public IHttpActionResult GetParty(int id)
        {
            Party party = db.Parties.Find(id);
            if (party == null)
            {
                return NotFound();
            }

            return Ok(party);
        }

        // PUT: api/Party/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParty(int id, Party party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != party.PartyID)
            {
                return BadRequest();
            }

            db.Entry(party).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartyExists(id))
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

        // POST: api/Party
        [ResponseType(typeof(Party))]
        public IHttpActionResult PostParty(Party party)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parties.Add(party);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PartyExists(party.PartyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = party.PartyID }, party);
        }

        // DELETE: api/Party/5
        [ResponseType(typeof(Party))]
        public IHttpActionResult DeleteParty(int id)
        {
            Party party = db.Parties.Find(id);
            if (party == null)
            {
                return NotFound();
            }

            db.Parties.Remove(party);
            db.SaveChanges();

            return Ok(party);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartyExists(int id)
        {
            return db.Parties.Count(e => e.PartyID == id) > 0;
        }
    }
}