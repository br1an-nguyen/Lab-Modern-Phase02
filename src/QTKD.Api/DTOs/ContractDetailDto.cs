using QTKD.Api.Domain;

namespace QTKD.Api.DTOs;

public sealed record ContractDetailDto(
    Guid Id, string ContractNo, Guid CustomerId, DateOnly? StartDate, DateOnly? EndDate,
    ContractStatus Status, decimal? Value, string? PaymentTerms, string? ServiceTerms,
    CustomerSummaryDto Customer, IReadOnlyList<ContractAttachmentDto> Attachments);

public sealed record ContractAttachmentDto(Guid Id, string FileName, string StorageKey);
