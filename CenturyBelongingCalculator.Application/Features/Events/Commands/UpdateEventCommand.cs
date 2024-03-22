using AutoMapper;
using CenturyBelongingCalculator.Application.Common;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public record UpdateEventCommand : ICommand<int>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string BeforeEventLabel { get; set; }
    public string AfterEventLabel { get; set; }
    public DateTimeOffset EventDate { get; set; }
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, int>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        var aEvent = await _eventRepository.GetEventByIdAsync(request.Id);
        if (aEvent == null)
        {
            return 0;
        }

        var eventObject = new Event
        {
            Description = request.Description,
            Id = request.Id,
            Name = request.Name,
            BeforeEventLabel = request.BeforeEventLabel,
            AfterEventLabel = request.AfterEventLabel,
            EventDate = request.EventDate,
        };

        return await _eventRepository.UpdateEventAsync(eventObject);
    }
}
