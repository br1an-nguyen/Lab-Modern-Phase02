using Microsoft.EntityFrameworkCore;
using QTKD.Api.Domain;

namespace QTKD.Api.Data;

public sealed class DevelopmentSeeder(QtkdDbContext db)
{
    public static readonly Guid MinhPhatId = Guid.Parse("11111111-1111-4111-8111-111111111111");
    public static readonly Guid AnBinhId = Guid.Parse("22222222-2222-4222-8222-222222222222");
    public static readonly Guid ContractId = Guid.Parse("33333333-3333-4333-8333-333333333333");
    public static readonly Guid PriceListId = Guid.Parse("44444444-4444-4444-8444-444444444444");
    public static readonly Guid HandlingId = Guid.Parse("55555555-5555-4555-8555-555555555555");
    public static readonly Guid StorageId = Guid.Parse("66666666-6666-4666-8666-666666666666");

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if ((await db.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
        {
            throw new InvalidOperationException("Run dotnet ef database update before seeding.");
        }

        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);
        foreach (var customer in new[]
        {
            new Customer { Id = MinhPhatId, Code = "KH-MP", Name = "Công ty Minh Phát", Status = CustomerStatus.Active },
            new Customer { Id = AnBinhId, Code = "KH-AB", Name = "Công ty An Bình", Status = CustomerStatus.Active }
        })
        {
            if (!await db.Customers.AnyAsync(x => x.Id == customer.Id, cancellationToken))
                db.Customers.Add(customer);
        }

        if (!await db.Contracts.AnyAsync(x => x.Id == ContractId, cancellationToken))
        {
            db.Contracts.Add(new Contract
            {
                Id = ContractId, ContractNo = "HD-2026-018", CustomerId = MinhPhatId,
                StartDate = new DateOnly(2026, 10, 1), EndDate = new DateOnly(2027, 9, 30),
                Status = ContractStatus.Draft,
                PaymentTerms = "Dữ liệu minh họa: thanh toán theo kỳ tháng."
                // ServiceTerms and attachments intentionally missing for Week 06's failed Submit demo.
            });
        }

        if (!await db.PriceLists.AnyAsync(x => x.Id == PriceListId, cancellationToken))
        {
            db.PriceLists.Add(new PriceList
            {
                Id = PriceListId, ContractId = ContractId, Name = "Bảng giá dịch vụ Minh Phát - bản nháp",
                EffectiveDate = new DateOnly(2026, 10, 1), EndDate = new DateOnly(2027, 9, 30),
                Version = 1, Status = PriceListStatus.Draft
            });
        }

        foreach (var item in new[]
        {
            new PriceItem { Id = HandlingId, PriceListId = PriceListId, ServiceCode = "BOC_XEP", UnitPrice = 250000m },
            new PriceItem { Id = StorageId, PriceListId = PriceListId, ServiceCode = "LUU_KHO", UnitPrice = 150000m }
        })
        {
            if (!await db.PriceItems.AnyAsync(x => x.Id == item.Id, cancellationToken))
                db.PriceItems.Add(item);
        }

        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }
}
