using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MiniSpiir.Models;

namespace MiniSpiir.Controllers
{
    public class PostingController : Controller
    {
        private readonly MiniSpiirDbContext _miniSpiirDbContext;

        public PostingController()
        {
            _miniSpiirDbContext = new MiniSpiirDbContext();
        }

        public ActionResult Index()
        {
            List<Posting> postings = _miniSpiirDbContext.Set<Posting>().OrderByDescending(p => p.Date).ToList();

            return View(postings);
        }

        public ActionResult Create(Posting posting)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Invalid posting!";
                return RedirectToAction("Index");
            }

            _miniSpiirDbContext.Postings.Add(posting);
            _miniSpiirDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}