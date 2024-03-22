using AutoMapper;
using CenturyBelongingCalculator.Application.Common;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public record CreateEventCommand : ICommand<EventModel>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string BeforeEventLabel { get; set; }
    public required string AfterEventLabel { get; set; }
    public DateTimeOffset EventDate { get; set; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventModel>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }
    public async Task<EventModel> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = new Event { 
            Description = request.Description, 
            Name = request.Name, 
            AfterEventLabel = request.AfterEventLabel,
            BeforeEventLabel = request.BeforeEventLabel,
            EventDate = request.EventDate.ToUniversalTime(), 
            Id = Guid.NewGuid() 
        };
        var result = await _eventRepository.CreateEventAsync(eventEntity);
        return _mapper.Map<EventModel>(result);
    }
}
