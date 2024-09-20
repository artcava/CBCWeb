namespace CenturyBelongingCalculator.Application.Features;

public class CalcModel
{
    public Guid Id { get; set; }
    public required string CalcName { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string EventDescription { get; set; } = string.Empty;
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EventEventDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public int DaysBeforeEvent { get { return (EventEventDate - StartDate).Days; } }
    public int DaysAfterEvent { get { return (EndDate - EventEventDate).Days; } }
    public DateTimeOffset JoinDate { get { return EventEventDate.AddDays(DaysBeforeEvent); } }
    public string EventBeforeEventLabel { get; set; } = string.Empty;
    public string EventAfterEventLabel { get; set; } = string.Empty;
}
