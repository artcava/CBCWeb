namespace CenturyBelongingCalculator.Domain;

public class Event
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public DateTimeOffset EventDate { get; set; }

}
