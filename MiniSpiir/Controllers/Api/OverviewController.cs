using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MiniSpiir.Models;

namespace MiniSpiir.Controllers.Api
{
    public class OverviewController : ApiController
    {
        private readonly MiniSpiirDbContext db = new MiniSpiirDbContext();

        // GET api/overview
        public IEnumerable<OverviewCategory> Get()
        {
            return db.Postings.GroupBy(p => p.Category)
                     .Select(g => new OverviewCategory {Total = g.Sum(p => p.Amount), Name = g.Key})
                     .OrderByDescending(o => o.Total)
                     .ToList();
        }
    }

    public class OverviewCategory
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
    }
}