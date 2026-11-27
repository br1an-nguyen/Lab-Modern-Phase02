namespace QTKD.Api.Domain;

public sealed class PriceList
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid ContractId { get; set; }
    public DateOnly EffectiveDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int Version { get; set; } = 1;
    public PriceListStatus Status { get; set; } = PriceListStatus.Draft;
    public Contract Contract { get; set; } = null!;
    public ICollection<PriceItem> Items { get; set; } = new List<PriceItem>();
}
