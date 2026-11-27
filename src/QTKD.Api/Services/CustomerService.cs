using Microsoft.EntityFrameworkCore;
using QTKD.Api.Data;
using QTKD.Api.DTOs;

namespace QTKD.Api.Services;

public interface ICustomerService
{
    Task<IReadOnlyList<CustomerSummaryDto>> GetCustomersAsync(CancellationToken cancellationToken);
}

public sealed class CustomerService(QtkdDbContext db) : ICustomerService
{
    public async Task<IReadOnlyList<CustomerSummaryDto>> GetCustomersAsync(CancellationToken cancellationToken)
        => await db.Customers.AsNoTracking().OrderBy(x => x.Code)
            .Select(x => new CustomerSummaryDto(x.Id, x.Code, x.Name, x.Status))
            .ToListAsync(cancellationToken);
}
