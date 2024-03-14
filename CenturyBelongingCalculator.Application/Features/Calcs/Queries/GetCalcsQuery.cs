using AutoMapper;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features;

public record GetCalcsQuery : IRequest<IEnumerable<CalcModel>>;

public class GetCalcsQueryHandler : IRequestHandler<GetCalcsQuery, IEnumerable<CalcModel>>
{
    private readonly ICalcRepository _calcRepository;
    private readonly IMapper _mapper;

    public GetCalcsQueryHandler(ICalcRepository calcRepository, IMapper mapper)
    {
        _calcRepository = calcRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CalcModel>> Handle(GetCalcsQuery request, CancellationToken cancellationToken)
    {
        var calcs = await _calcRepository.GetAllCalcsAsync();
        var calcList = _mapper.Map<IEnumerable<CalcModel>>(calcs);
        return calcList;
    }
}
