namespace QTKD.Api.Domain;

// Metadata for the lab only. This does not upload or verify a physical file.
public sealed class ContractAttachment
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string StorageKey { get; set; } = string.Empty;
    public Contract Contract { get; set; } = null!;
}
