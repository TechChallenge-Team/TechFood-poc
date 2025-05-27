using Microsoft.AspNetCore.Mvc;
using TechFood.Application.Models.Customer;
using TechFood.Application.UseCases.Interfaces;

namespace TechFood.Api.Controllers;

[ApiController()]
[Route("v1/[controller]")]
public class CustomersController(ICustomerUseCase customerUseCase) : ControllerBase
{
    private readonly ICustomerUseCase _customerUseCase = customerUseCase;

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequest request)
    {
        var result = await _customerUseCase.CreateCustomerAsync(request);

        return Ok(result);
    }

    [HttpGet("{document}")]
    public async Task<IActionResult> GetByDocumentAsync(string document)
    {
        var result = await _customerUseCase.GetByDocumentAsync(TechFood.Domain.Enums.DocumentType.CPF.ToString(), document);
    
        return result != null ? Ok(result) : NotFound();
    }
}
