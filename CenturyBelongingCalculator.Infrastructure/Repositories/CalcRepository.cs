using CenturyBelongingCalculator.Domain;
using Microsoft.EntityFrameworkCore;

namespace CenturyBelongingCalculator.Infrastructure;
public class CalcRepository : ICalcRepository
{
    private readonly CenturyBelongingCalculatorDbContext _calcDbContext;
    public CalcRepository(CenturyBelongingCalculatorDbContext calcDbContext)
    {
        _calcDbContext = calcDbContext;
    }

    public async Task<Calc> CreateCalcAsync(Calc calcObj)
    {
        await _calcDbContext.Calcs.AddAsync(calcObj);
        await _calcDbContext.SaveChangesAsync();
        return calcObj;
    }

    public async Task<int> DeleteCalcAsync(Guid calcId)
    {
        return await _calcDbContext.Calcs
            .Where(c => c.Id == calcId)
            .ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Calc>> GetAllCalcsAsync()
    {
        return await _calcDbContext.Calcs.ToListAsync();
    }

    public async Task<Calc> GetCalcByIdAsync(Guid calcId)
    {
        return await _calcDbContext.Calcs.AsNoTracking()
            .Include(c => c.Event)
            .FirstOrDefaultAsync(c => c.Id == calcId);
    }

    public async Task<Calc> GetCalcByUserAsync(string userId)
    {
        return await _calcDbContext.Calcs.AsNoTracking()
            .Include(c => c.Event)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<int> UpdateCalcAsync(Calc calcObj)
    {
        return await _calcDbContext.Calcs
            .Where(c => c.Id == calcObj.Id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(c => c.CalcName, calcObj.CalcName)
                .SetProperty(c => c.StartDate, calcObj.StartDate)
                .SetProperty(c => c.EndDate, calcObj.EndDate)
                );
    }
}
