using Microsoft.AspNetCore.Mvc;

namespace NMHAssignment.WebAPI.Models
{
    public class CalculationRequest
    {
        [FromRoute(Name = "key")]
        public int Key { get; set; }

        [FromBody]
        public CalculationRequestBody Body { get; set; } = new();
    }

    public class CalculationRequestBody
    {
        public decimal? Input { get; set; }
    }
}
