using Microsoft.EntityFrameworkCore;
using QTKD.Api.Data;
using QTKD.Api.DTOs;

namespace QTKD.Api.Services;

public interface IContractService
{
    Task<ContractDetailDto?> GetContractAsync(Guid id, CancellationToken cancellationToken);
}

public sealed class ContractService(QtkdDbContext db) : IContractService
{
    public async Task<ContractDetailDto?> GetContractAsync(Guid id, CancellationToken cancellationToken)
        => await db.Contracts.AsNoTracking().Where(x => x.Id == id)
            .Select(x => new ContractDetailDto(
                x.Id, x.ContractNo, x.CustomerId, x.StartDate, x.EndDate, x.Status,
                x.Value, x.PaymentTerms, x.ServiceTerms,
                new CustomerSummaryDto(x.Customer.Id, x.Customer.Code, x.Customer.Name, x.Customer.Status),
                x.Attachments.OrderBy(a => a.Id)
                    .Select(a => new ContractAttachmentDto(a.Id, a.FileName, a.StorageKey)).ToList()))
            .SingleOrDefaultAsync(cancellationToken);
}
