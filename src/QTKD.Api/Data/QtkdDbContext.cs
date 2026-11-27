using Microsoft.EntityFrameworkCore;
using QTKD.Api.Domain;

namespace QTKD.Api.Data;

public sealed class QtkdDbContext(DbContextOptions<QtkdDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<PriceList> PriceLists => Set<PriceList>();
    public DbSet<PriceItem> PriceItems => Set<PriceItem>();
    public DbSet<ContractAttachment> ContractAttachments => Set<ContractAttachment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var customer = modelBuilder.Entity<Customer>();
        customer.HasKey(x => x.Id);
        customer.Property(x => x.Code).IsRequired();
        customer.Property(x => x.Name).IsRequired();
        customer.Property(x => x.Status).HasConversion<string>().IsRequired();
        customer.HasIndex(x => x.Code).IsUnique();
        customer.ToTable("Customers", table => table.HasCheckConstraint(
            "CK_Customers_Status", "\"Status\" IN ('Active', 'Inactive')"));

        var contract = modelBuilder.Entity<Contract>();
        contract.HasKey(x => x.Id);
        contract.Property(x => x.ContractNo).IsRequired();
        contract.Property(x => x.Status).HasConversion<string>().IsRequired();
        contract.Property(x => x.Value).HasPrecision(18, 2);
        contract.HasIndex(x => x.ContractNo).IsUnique();
        contract.HasOne(x => x.Customer).WithMany(x => x.Contracts)
            .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
        contract.ToTable("Contracts", table => table.HasCheckConstraint(
            "CK_Contracts_Status",
            "\"Status\" IN ('Draft', 'PendingApproval', 'RevisionRequested', 'Rejected', 'Approved', 'Active', 'Cancelled', 'Expired')"));

        var priceList = modelBuilder.Entity<PriceList>();
        priceList.HasKey(x => x.Id);
        priceList.Property(x => x.Name).IsRequired();
        priceList.Property(x => x.Status).HasConversion<string>().IsRequired();
        priceList.HasOne(x => x.Contract).WithMany(x => x.PriceLists)
            .HasForeignKey(x => x.ContractId).OnDelete(DeleteBehavior.Restrict);
        priceList.ToTable("PriceLists", table => table.HasCheckConstraint(
            "CK_PriceLists_Status",
            "\"Status\" IN ('Draft', 'Submitted', 'Approved', 'Effective', 'Superseded', 'Rejected', 'Expired')"));

        var priceItem = modelBuilder.Entity<PriceItem>();
        priceItem.HasKey(x => x.Id);
        priceItem.Property(x => x.ServiceCode).IsRequired();
        priceItem.Property(x => x.UnitPrice).HasPrecision(18, 2);
        priceItem.HasOne(x => x.PriceList).WithMany(x => x.Items)
            .HasForeignKey(x => x.PriceListId).OnDelete(DeleteBehavior.Restrict);

        var attachment = modelBuilder.Entity<ContractAttachment>();
        attachment.HasKey(x => x.Id);
        attachment.Property(x => x.FileName).IsRequired();
        attachment.Property(x => x.StorageKey).IsRequired();
        attachment.HasOne(x => x.Contract).WithMany(x => x.Attachments)
            .HasForeignKey(x => x.ContractId).OnDelete(DeleteBehavior.Restrict);
    }
}
