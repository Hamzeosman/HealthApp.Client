namespace Mintakt.Client.Models
{
    public class UserProfile
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? Age { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? HeightCm { get; set; }
        public string FitnessLevel { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateProfileRequest
    {
        public int? Age { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? HeightCm { get; set; }
        public string FitnessLevel { get; set; } = string.Empty;
        public string Goal { get; set; } = string.Empty;
    }
}
