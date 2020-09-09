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
    public class VoteController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Vote
        public IQueryable<Vote> GetVotes()
        {
            return db.Votes;
        }

        // GET: api/Vote/5
        [ResponseType(typeof(Vote))]
        public IHttpActionResult GetVote(long id)
        {
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return NotFound();
            }

            return Ok(vote);
        }

        // PUT: api/Vote/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVote(long id, Vote vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vote.VoteID)
            {
                return BadRequest();
            }

            db.Entry(vote).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteExists(id))
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

        // POST: api/Vote
        [ResponseType(typeof(Vote))]
        public IHttpActionResult PostVote(Vote vote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Votes.Add(vote);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vote.VoteID }, vote);
        }

        // DELETE: api/Vote/5
        [ResponseType(typeof(Vote))]
        public IHttpActionResult DeleteVote(long id)
        {
            Vote vote = db.Votes.Find(id);
            if (vote == null)
            {
                return NotFound();
            }

            db.Votes.Remove(vote);
            db.SaveChanges();

            return Ok(vote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoteExists(long id)
        {
            return db.Votes.Count(e => e.VoteID == id) > 0;
        }
    }
}