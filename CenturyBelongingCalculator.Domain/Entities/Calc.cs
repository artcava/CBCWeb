using System.ComponentModel.DataAnnotations;

namespace CenturyBelongingCalculator.Domain
{
    public class Calc
    {
        public Guid Id { get; set; }
        [MaxLength(450)]
        public required string UserId { get; set; }
        public required string CalcName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; } = DateTimeOffset.Now;
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
