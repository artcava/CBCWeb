using CenturyBelongingCalculator.Application.Common;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public record DeleteEventCommand : ICommand<int>
{
    public Guid Id { get; set; }
}

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, int>
{
    private readonly IEventRepository _eventRepository;
    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<int> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventObj = await _eventRepository.GetEventByIdAsync(request.Id);
        if (eventObj == null)
        {
            return 0;
        }

        return await _eventRepository.DeleteEventAsync(request.Id);
    }
}
