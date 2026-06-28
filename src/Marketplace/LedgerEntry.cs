namespace Marketplace;

/// <summary>
/// One entry in a wallet's append-only ledger. Each entry knows how it changes a running
/// balance when the ledger is replayed as of a given date. New entry types (deposits,
/// withdrawals, fees, escrow releases…) should be addable by subclassing this — without
/// editing the existing ones.
/// </summary>
public abstract class LedgerEntry
{
    public decimal Amount { get; }

    protected LedgerEntry(decimal amount) => Amount = amount;

    /// <summary>The balance after this entry is applied, evaluated as of <paramref name="asOf"/>.</summary>
    public abstract decimal ApplyTo(decimal balance, DateOnly asOf);
}
