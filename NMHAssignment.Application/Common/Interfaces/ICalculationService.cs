using NMHAssignment.Application.DTOs;

namespace NMHAssignment.Application.Common.Interfaces
{
    public interface ICalculationService
    {
        Task<CalculationDTO> CalculateAsync(int key, decimal input);
    }
}
