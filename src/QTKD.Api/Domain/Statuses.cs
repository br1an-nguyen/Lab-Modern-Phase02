namespace QTKD.Api.Domain;

public enum CustomerStatus { Active, Inactive }

// PendingApproval represents the pending review stage in the Phase 2 lab.
// Week 05 defines the vocabulary only; state transitions are not implemented yet.
public enum ContractStatus
{
    Draft, PendingApproval, RevisionRequested, Rejected, Approved, Active, Cancelled, Expired
}

public enum PriceListStatus { Draft, Submitted, Approved, Effective, Superseded, Rejected, Expired }
