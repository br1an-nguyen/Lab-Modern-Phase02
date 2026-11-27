using Microsoft.AspNetCore.Mvc;
using QTKD.Api.DTOs;
using QTKD.Api.Services;

namespace QTKD.Api.Controllers;

[ApiController]
[Route("api/contracts")]
public sealed class ContractsController(IContractService service) : ControllerBase
{
    // Do not use a :guid route constraint: malformed UUIDs should bind to a 400, not a 404.
    [HttpGet("{id}")]
    [ProducesResponseType<ContractDetailDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContractDetailDto>> Get(Guid id, CancellationToken cancellationToken)
    {
        var contract = await service.GetContractAsync(id, cancellationToken);
        return contract is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Contract not found.",
                detail: "No contract exists for the supplied id.")
            : Ok(contract);
    }
}
