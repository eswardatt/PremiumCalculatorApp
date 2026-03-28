using premium_calculator_api.Models;

namespace premium_calculator_api.Services
{
    public class PremiumService:IPremiumService
    {
        private readonly List<Rating> _ratingFactors = new()
    {
        new Rating { RatingName = "Professional", Factor = 1.5M },
        new Rating { RatingName = "White Collar", Factor = 2.25M },
        new Rating { RatingName = "Light Manual", Factor = 11.50M },
        new Rating { RatingName = "Heavy Manual", Factor = 31.75M }
    };

        private readonly List<Occupation> _occupations = new()
    {
        new Occupation { Id = 1, Name = "Cleaner", Rating = "Light Manual" },
        new Occupation { Id = 2, Name = "Doctor", Rating = "Professional" },
        new Occupation { Id = 3, Name = "Author", Rating = "White Collar" },
        new Occupation { Id = 4, Name = "Farmer", Rating = "Heavy Manual" },
        new Occupation { Id = 5, Name = "Mechanic", Rating = "Heavy Manual" },
        new Occupation { Id = 6, Name = "Florist", Rating = "Light Manual" },
        new Occupation { Id = 7, Name = "Other", Rating = "Heavy Manual" }
    };

        public decimal CalculatePremium(PremiumRequest req)
        {
            var occupation = _occupations.First(x => x.Id == req.OccupationId);
            var factor = _ratingFactors.First(r => r.RatingName == occupation.Rating).Factor;

            var premium = (req.DeathSumInsured * factor * req.AgeNextBirthday) / 1000 * 12;

            return Math.Round(premium, 2);
        }

        public List<Occupation> GetOccupations() => _occupations;
    }

}
