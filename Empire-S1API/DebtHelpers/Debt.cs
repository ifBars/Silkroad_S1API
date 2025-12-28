using Newtonsoft.Json;

namespace Empire.DebtHelpers    
{
    public class Debt
    {
        [JsonProperty("total_debt")]
        public float TotalDebt { get; set; } = 0f;
        [JsonProperty("interest_rate")]
        public float InterestRate { get; set; } = 0f;
        [JsonProperty("day_multiple")]
        public float DayMultiple { get; set; } = 0f;
        [JsonProperty("day_exponent")]
        public float DayExponent { get; set; } = 0f;
        [JsonProperty("product_bonus")]
        public float ProductBonus { get; set; } = 0f;
    }
}