using AutoMapper;
using CenturyBelongingCalculator.Application.Common;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public class CreateCalcCommand : ICommand<CalcModel>
{
    public required string CalcName { get; set; }
    public required string UserId {  get; set; }
    public Guid EventId { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; } = DateTimeOffset.Now;
}

public class CreateCalcCommandHandler : IRequestHandler<CreateCalcCommand, CalcModel>
{
    private readonly ICalcRepository _calcRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public CreateCalcCommandHandler(ICalcRepository calcRepository, IEventRepository eventRepository, IMapper mapper)
    {
        _calcRepository = calcRepository;
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<CalcModel> Handle(CreateCalcCommand request, CancellationToken cancellationToken)
    {
        #region Checks
        if (request.StartDate >= request.EndDate)
            throw new EndDateGreaterThenStartDateException();

        var aevent = await _eventRepository.GetEventByIdAsync(request.EventId);
        if (aevent == null)
            throw new NoEventExistsException(request.EventId);

        if ((request.StartDate >= aevent.EventDate) || (aevent.EventDate >= request.EndDate))
            throw new NotAllowedCalcException(aevent.Name);
        #endregion


        var result = await _calcRepository.CreateCalcAsync(new Calc
        {
            CalcName = request.CalcName,
            UserId = request.UserId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            EventId = request.EventId,
            Id = Guid.NewGuid()
        });

        return _mapper.Map<CalcModel>(result);
    }
}
