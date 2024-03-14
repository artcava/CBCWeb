using AutoMapper;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public record GetDefaultCalcQuery : IRequest<CalcModel>;

public class GetDefaultCalcsQueryHandler: IRequestHandler<GetDefaultCalcQuery, CalcModel>
{
    private readonly ICalcRepository _calcRepository;
    private readonly IMapper _mapper;

    public GetDefaultCalcsQueryHandler(ICalcRepository calcRepository, IMapper mapper)
    {
        _calcRepository = calcRepository;
        _mapper = mapper;
    }

    public async Task<CalcModel> Handle(GetDefaultCalcQuery request, CancellationToken cancellationToken)
    {
        var calc = new CalcModel
        {
            CalcName = "Maravigliosa Creatura",
            EventName = "Century belonging",
            EventDescription = "This calculates how many days a Person has lived in the two adjacent centuries.</br>" +
                "One of BirthDate and the next.</br>" +
                "The JoinDate represents the day on which the Person has lived 50% of their time across the two centuries.",
            EventDate = new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero),
            StartDate = new DateTimeOffset(1969, 3, 14, 0, 0, 0, TimeSpan.Zero),
            EndDate = DateTimeOffset.UtcNow,
            Id = Guid.NewGuid()
        };
        return calc;
        //var calcList = _mapper.Map<CalcModel>(calc);
        //return calcList;
    }
}
