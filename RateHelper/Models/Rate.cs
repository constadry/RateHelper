using System;

namespace RateHelper.Models
{
    public class Rate
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public double Value { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}