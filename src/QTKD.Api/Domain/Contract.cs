namespace QTKD.Api.Domain;

public sealed class Contract
{
    public Guid Id { get; set; }
    public string ContractNo { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    // Incomplete drafts are allowed. Submit validation belongs to Week 06.
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public ContractStatus Status { get; set; } = ContractStatus.Draft;
    public decimal? Value { get; set; }
    public string? PaymentTerms { get; set; }
    public string? ServiceTerms { get; set; }
    public Customer Customer { get; set; } = null!;
    public ICollection<PriceList> PriceLists { get; set; } = new List<PriceList>();
    public ICollection<ContractAttachment> Attachments { get; set; } = new List<ContractAttachment>();
}
