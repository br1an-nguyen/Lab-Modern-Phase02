namespace QTKD.Api.Domain;

public sealed class PriceItem
{
    public Guid Id { get; set; }
    public Guid PriceListId { get; set; }
    public string ServiceCode { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public PriceList PriceList { get; set; } = null!;
}
