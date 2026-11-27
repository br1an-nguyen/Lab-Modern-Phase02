using Microsoft.AspNetCore.Mvc;
using QTKD.Api.DTOs;
using QTKD.Api.Services;

namespace QTKD.Api.Controllers;

[ApiController]
[Route("api/customers")]
public sealed class CustomersController(ICustomerService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CustomerSummaryDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerSummaryDto>>> Get(CancellationToken cancellationToken)
        => Ok(await service.GetCustomersAsync(cancellationToken));
}
