using System.Data.Entity;

namespace MiniSpiir.Models
{
    public class MiniSpiirDbContext : DbContext
    {
        public DbSet<Posting> Postings { get; set; }
    }
}