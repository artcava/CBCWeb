using CenturyBelongingCalculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace CenturyBelongingCalculator.Infrastructure;

public class EventRepository : IEventRepository
{
    private readonly CenturyBelongingCalculatorDbContext _eventDbContext;
    public EventRepository(CenturyBelongingCalculatorDbContext eventDbContext)
    {
        _eventDbContext = eventDbContext;
    }

    public async Task<Event> CreateEventAsync(Event eventObj)
    {
        await _eventDbContext.Events.AddAsync(eventObj);
        await _eventDbContext.SaveChangesAsync();
        return eventObj;
    }

    public async Task<int> DeleteEventAsync(Guid eventId)
    {
        return await _eventDbContext.Events
            .Where(e => e.Id == eventId)
            .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        return await _eventDbContext.Events.ToListAsync();
    }

    public async Task<Event> GetEventByIdAsync(Guid eventId)
    {
        return await _eventDbContext.Events.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == eventId);
    }

    public async Task<int> UpdateEventAsync(Event eventObj)
    {
        return await _eventDbContext.Events
            .Where(e => e.Id == eventObj.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(e => e.Name, eventObj.Name)
                .SetProperty(e => e.Description, eventObj.Description)
                .SetProperty(e => e.EventDate, eventObj.EventDate)
            );
    }
}
