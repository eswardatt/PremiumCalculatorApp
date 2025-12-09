namespace premium_calculator_api.Models
{
    public class PremiumRequest
    {
        public string Name { get; set; }
        public int AgeNextBirthday { get; set; }
        public string Dob { get; set; }  // MM/YYYY
        public int OccupationId { get; set; }
        public decimal DeathSumInsured { get; set; }
    }
}
