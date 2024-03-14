namespace CenturyBelongingCalculator.Domain
{
    public class Calc
    {
        public Guid Id { get; set; }
        public required string CalcName { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; } = DateTimeOffset.Now;
        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
