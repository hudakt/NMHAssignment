using Microsoft.Extensions.Caching.Memory;
using NMHAssignment.Application.Common.Interfaces;
using NMHAssignment.Application.DTOs;

namespace NMHAssignment.Application.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMessageHub _messageHub;
        private readonly SemaphoreSlim _semaphore;

        public CalculationService(IMemoryCache memoryCache, IMessageHub messageHub)
        {
            _memoryCache = memoryCache;
            _messageHub = messageHub;
            _semaphore = new SemaphoreSlim(1, 1);
        }

        public async Task<CalculationDTO> CalculateAsync(int key, decimal input)
        {
            if (key < 0) throw new ArgumentException($"{nameof(key)} argument has to be positive whole number.");
            if (input < 0) throw new ArgumentException($"{nameof(input)} argument has to be positive real number.");

            decimal valueToStore = 2;
            Calculation? storedValue;

            await _semaphore.WaitAsync();
            try
            {
                if (_memoryCache.TryGetValue(key, out storedValue))
                {
                    valueToStore = storedValue!.IsExpired ? 2 : ComputeValueToStore(input, storedValue!.Value);
                }

                _memoryCache.Set(key, new Calculation(valueToStore, DateTimeOffset.UtcNow.AddSeconds(15)));
            }
            finally
            {
                _semaphore.Release();
            }

            var result = new CalculationDTO
            {
                ComputedValue = valueToStore,
                InputValue = input,
                PreviousValue = storedValue?.Value
            };

            _messageHub.Publish(result, "calculation_queue");

            return result;
        }

        private decimal ComputeValueToStore(decimal input, decimal storedValue)
        {
            var divisionResult = input / storedValue;
            var logResult = Math.Log(Convert.ToDouble(divisionResult));
            var resultSqrt = Math.Pow(logResult, (double)1 / 3);

            return Convert.ToDecimal(resultSqrt);
        }

        private class Calculation
        {
            public decimal Value { get; }
            public bool IsExpired => expiration.UtcDateTime < DateTime.UtcNow;

            private DateTimeOffset expiration;

            public Calculation(decimal value, DateTimeOffset expiresAt)
            {
                Value = value;
                expiration = expiresAt;
            }
        }
    }
}
