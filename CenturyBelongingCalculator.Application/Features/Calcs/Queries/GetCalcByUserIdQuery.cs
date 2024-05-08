using AutoMapper;
using CenturyBelongingCalculator.Domain;
using MediatR;

namespace CenturyBelongingCalculator.Application.Features.Calcs.Queries
{
    public record GetCalcByUserIdQuery : IRequest<CalcModel>
    {
        public required string Id { get; set; }
    }

    public class GetCalcByUserIdQueryHandler : IRequestHandler<GetCalcByUserIdQuery, CalcModel>
    {
        private readonly ICalcRepository _calcRepository;
        private readonly IMapper _mapper;

        public GetCalcByUserIdQueryHandler(ICalcRepository calcRepository, IMapper mapper)
        {
            _calcRepository = calcRepository;
            _mapper = mapper;
        }

        public async Task<CalcModel> Handle(GetCalcByUserIdQuery request, CancellationToken cancellationToken)
        {
            var calc = await _calcRepository.GetCalcByUserAsync(request.Id);
            return _mapper.Map<CalcModel>(calc);
        }
    }
}
