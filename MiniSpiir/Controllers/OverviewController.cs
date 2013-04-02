using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MiniSpiir.Models;

namespace MiniSpiir.Controllers
{
    public class OverviewController : Controller
    {
        private readonly MiniSpiirDbContext _miniSpiirDbContext;

        public OverviewController()
        {
            _miniSpiirDbContext = new MiniSpiirDbContext();
        }

        public ActionResult Index()
        {
            List<Posting> postings = _miniSpiirDbContext.Postings.ToList();

            var categories = postings.GroupBy(p => p.Category)
                                     .Select(g => new {y = g.Sum(p => p.Amount), name = g.Key})
                                     .OrderByDescending(o => o.y)
                                     .ToList();

            return View(new OverviewViewModel { ChartJson = new JavaScriptSerializer().Serialize(categories) });
        }
    }

    public class OverviewViewModel 
    {
        public string ChartJson { get; set; }
    }
}