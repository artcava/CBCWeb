using CenturyBelongingCalculator.Application.Common;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public class GetDaysToJoinDateQuery : IQuery<int>
{
    public DateTimeOffset StartDate { get; set; }
    public Guid EventId { get; set; }
}
public class GetDaysToJoinDateHandler : IRequestHandler<GetDaysToJoinDateQuery, int>
{
    private readonly IEventRepository _eventRepository;

    public GetDaysToJoinDateHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<int> Handle(GetDaysToJoinDateQuery request, CancellationToken cancellationToken)
    {
        var aevent = await _eventRepository.GetEventByIdAsync(request.EventId);
        if (aevent == null)
            throw new NoEventExistsException(request.EventId);

        if (request.StartDate >= aevent.EventDate)
            throw new NotAllowedCalcException(aevent.Name);

        var _now = DateTimeOffset.UtcNow;
        var joinDate = aevent.EventDate.AddDays((aevent.EventDate - request.StartDate).Days);
        var result = (joinDate - _now).Days;
        if (result <= 0)
            throw new JoinDateElapsedException(aevent.Name);

        return result;
    }
}
