using AutoMapper;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public record GetEventsQuery : IRequest<IEnumerable<EventModel>>;

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<EventModel>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public GetEventsQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<EventModel>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetAllEventsAsync();
        var eventList = _mapper.Map<IEnumerable<EventModel>>(events);
        return eventList;
    }
}
