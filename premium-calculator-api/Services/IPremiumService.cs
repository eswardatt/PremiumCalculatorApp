using premium_calculator_api.Models;

namespace premium_calculator_api.Services
{
    public interface IPremiumService
    {
        decimal CalculatePremium(PremiumRequest req);
        List<Occupation> GetOccupations();
    }
}
