using CenturyBelongingCalculator.Application.Common;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public class GetJoinDateQuery : IQuery<DateTimeOffset>
{
    public DateTimeOffset StartDate { get; set; }
    public Guid EventId { get; set; }
}

public class GetJoinDateHandler : IRequestHandler<GetJoinDateQuery, DateTimeOffset>
{
    private readonly IEventRepository _eventRepository;

    public GetJoinDateHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<DateTimeOffset> Handle(GetJoinDateQuery request, CancellationToken cancellationToken)
    {
        var aevent = await _eventRepository.GetEventByIdAsync(request.EventId);
        if (aevent == null)
            throw new NoEventExistsException(request.EventId);

        if (request.StartDate >= aevent.EventDate)
            throw new NotAllowedCalcException(aevent.Name);

        var result = aevent.EventDate.AddDays((aevent.EventDate - request.StartDate).Days);

        return result;
    }
}

