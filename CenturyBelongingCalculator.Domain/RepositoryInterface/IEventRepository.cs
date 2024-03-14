namespace CenturyBelongingCalculator.Domain;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<Event> GetEventByIdAsync(Guid eventId);
    Task<Event> CreateEventAsync(Event eventObj);
    Task<int> UpdateEventAsync(Event eventObj);
    Task<int> DeleteEventAsync(Guid eventId);
}
