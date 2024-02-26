using NMHAssignment.Application.DTOs;

namespace NMHAssignment.Application.Common.Interfaces
{
    public interface ICalculationService
    {
        CalculationDTO Calculate(int key, decimal input);
    }
}
