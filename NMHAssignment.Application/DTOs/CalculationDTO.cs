namespace NMHAssignment.Application.DTOs
{
    public class CalculationDTO
    {
        public decimal ComputedValue { get; set; }
        public decimal InputValue { get; set; }
        public decimal? PreviousValue { get; set; }

        public override string ToString()
        {
            return $"Input: {InputValue}, Computed value: {ComputedValue}, PreviousValue: {PreviousValue}";
        }
    }
}
