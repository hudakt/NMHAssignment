using NMHAssignment.Application.DTOs;
using System.Text.Json.Serialization;

namespace NMHAssignment.WebAPI.Models
{
    public class CalculationResponse
    {
        [JsonPropertyName("computed_value")]
        public decimal ComputedValue { get; set; }

        [JsonPropertyName("input_value")]
        public decimal InputValue { get; set; }

        [JsonPropertyName("previous_value")]
        public decimal? PreviousValue { get; set; }

        public static CalculationResponse FromDto(CalculationDTO dto) => 
            new CalculationResponse
            { 
                ComputedValue = dto.ComputedValue, 
                InputValue = dto.InputValue, 
                PreviousValue = dto.PreviousValue
            };
    }
}
