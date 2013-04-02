using System;

namespace MiniSpiir.Models
{
    public class Posting
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Text { get; set; }

        public decimal Amount { get; set; }

        public string Category { get; set; }
    }
}