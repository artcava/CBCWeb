using AutoMapper;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public record GetEventByIdQuery : IRequest<EventModel>
{
    public Guid Id { get; set; }
}

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventModel>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public GetEventByIdQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }
    public async Task<EventModel> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var aEvent = await _eventRepository.GetEventByIdAsync(request.Id);
        var eventModel = _mapper.Map<EventModel>(aEvent);
        return eventModel;
    }
}
