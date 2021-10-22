using System;

namespace WebApplication1.Models
{
    public class Card : ICard
    {
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
