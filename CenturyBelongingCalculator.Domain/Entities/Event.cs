using System.ComponentModel.DataAnnotations;

namespace CenturyBelongingCalculator.Domain;

public class Event
{
    public Guid Id { get; set; }
    [MaxLength(64)]
    public required string Name { get; set; }
    [MaxLength(512)]
    public required string Description { get; set; }
    [MaxLength(32)]
    public string? BeforeEventLabel {  get; set; }
    [MaxLength(32)]
    public string? AfterEventLabel { get; set; }
    public DateTimeOffset EventDate { get; set; }

}
