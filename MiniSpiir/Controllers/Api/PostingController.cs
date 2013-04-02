using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MiniSpiir.Models;

namespace MiniSpiir.Controllers.Api
{
    public class PostingController : ApiController
    {
        private MiniSpiirDbContext db = new MiniSpiirDbContext();

        // GET api/PostingApi
        public IEnumerable<Posting> GetPostings()
        {
            return db.Postings.AsEnumerable();
        }

        // GET api/PostingApi/5
        public Posting GetPosting(int id)
        {
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return posting;
        }

        // PUT api/PostingApi/5
        public HttpResponseMessage PutPosting(int id, Posting posting)
        {
            if (ModelState.IsValid && id == posting.Id)
            {
                db.Entry(posting).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/PostingApi
        public HttpResponseMessage PostPosting(Posting posting)
        {
            if (ModelState.IsValid)
            {
                db.Postings.Add(posting);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, posting);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = posting.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/PostingApi/5
        public HttpResponseMessage DeletePosting(int id)
        {
            Posting posting = db.Postings.Find(id);
            if (posting == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Postings.Remove(posting);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, posting);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}