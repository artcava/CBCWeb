using System.Reflection.Metadata.Ecma335;

namespace CenturyBelongingCalculator.Application.Features;

public class CalcModel
{
    public Guid Id { get; set; }
    public required string CalcName { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string EventDescription { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EventDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int DaysBeforeEvent { get { return (EventDate - StartDate).Days; } }
    public int DaysAfterEvent { get { return (EndDate - EventDate).Days; } }
    public DateTimeOffset JoinDate { get{ return EventDate.AddDays(DaysBeforeEvent);  } }
}
