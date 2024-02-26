using Microsoft.Extensions.Caching.Memory;
using NMHAssignment.Application.Common.Interfaces;
using NMHAssignment.Application.DTOs;

namespace NMHAssignment.Application.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IMemoryCache _memoryCache;

        public CalculationService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public CalculationDTO Calculate(int key, decimal input)
        {
            if (key < 0) throw new ArgumentException($"{nameof(key)} argument has to be positive whole number.");
            if (input < 0) throw new ArgumentException($"{nameof(input)} argument has to be positive real number.");

            decimal valueToStore = 2;

            if (_memoryCache.TryGetValue(key, out decimal? storedValue))
            {
                valueToStore = ComputeValueToStore(input, storedValue!.Value);
            }

            _memoryCache.Set(key, valueToStore, TimeSpan.FromSeconds(15));

            return new CalculationDTO
            {
                ComputedValue = valueToStore,
                InputValue = input,
                PreviousValue = storedValue
            };
        }

        private decimal ComputeValueToStore(decimal input, decimal storedValue)
        {
            var divisionResult = input / storedValue;
            var logResult = Math.Log(Convert.ToDouble(divisionResult));
            var resultSqrt = Math.Pow(logResult, (double)1 / 3);

            return Convert.ToDecimal(resultSqrt);
        }
    }
}
