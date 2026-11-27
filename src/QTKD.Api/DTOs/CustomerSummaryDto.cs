using QTKD.Api.Domain;

namespace QTKD.Api.DTOs;

public sealed record CustomerSummaryDto(Guid Id, string Code, string Name, CustomerStatus Status);
