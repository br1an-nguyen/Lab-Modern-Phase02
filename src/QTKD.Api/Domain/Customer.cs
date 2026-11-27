namespace QTKD.Api.Domain;

public sealed class Customer
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? TaxCode { get; set; }
    public string? Address { get; set; }
    public string? RepresentativeName { get; set; }
    public string? ContactInfo { get; set; }
    public CustomerStatus Status { get; set; } = CustomerStatus.Active;
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
