namespace CenturyBelongingCalculator.Domain;

public interface ICalcRepository
{
    Task<IEnumerable<Calc>> GetAllCalcsAsync();
    Task<Calc> GetCalcByIdAsync(Guid calcId);
    Task<Calc> GetCalcByUserAsync(string userId);
    Task<Calc> CreateCalcAsync(Calc calcObj);
    Task<int> UpdateCalcAsync(Calc calcObj);
    Task<int> DeleteCalcAsync(Guid calcId);
}
