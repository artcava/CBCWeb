using System.ComponentModel;

namespace CenturyBelongingCalculator.Application.Features;

public class EventModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTimeOffset EventDate { get; set; }
}
