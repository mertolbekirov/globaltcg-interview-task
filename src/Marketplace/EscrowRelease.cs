namespace Marketplace;

/// <summary>
/// Funds held in escrow from a sale by a private seller. They only become available in the
/// seller's balance on or after the date the buyer is expected to confirm receipt
/// (<see cref="ReleaseOn"/>). Before that date the entry contributes nothing.
/// </summary>
public sealed class EscrowRelease : LedgerEntry
{
    public DateOnly ReleaseOn { get; }

    public EscrowRelease(decimal amount, DateOnly releaseOn) : base(amount) => ReleaseOn = releaseOn;

    public override decimal ApplyTo(decimal balance, DateOnly asOf)
    {
        // TODO (Part 3): release the escrowed amount on/after ReleaseOn.
        // - Before ReleaseOn: the balance is unchanged.
        // - On/after ReleaseOn: the escrowed Amount is added to the balance.
        throw new NotImplementedException("EscrowRelease.ApplyTo is not implemented yet — see TASK.md, Part 3.");
    }
}
