using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NMHAssignment.Application.Common.Interfaces;
using NMHAssignment.WebAPI.Models;

namespace NMHAssignment.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : ControllerBase
    {
        private readonly ICalculationService _calculationService;
        private readonly IValidator<CalculationRequest> _validator;

        public CalculationController(ICalculationService calculationService, IValidator<CalculationRequest> validator)
        {
            _calculationService = calculationService;
            _validator = validator;
        }

        [Route("{key:int}")]
        public async Task<IActionResult> Post(CalculationRequest request)
        {
            var validation = await _validator.ValidateAsync(request);

            if (!validation.IsValid)
                return BadRequest(validation.Errors);

            var result = _calculationService.Calculate(request.Key, request.Body.Input!.Value);

            return Ok(CalculationResponse.FromDto(result));
        }
    }
}
