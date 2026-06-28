namespace Marketplace;

/// <summary>An immediate, unconditional credit to the wallet (e.g. a bank top-up).</summary>
public sealed class Deposit : LedgerEntry
{
    public Deposit(decimal amount) : base(amount) { }

    public override decimal ApplyTo(decimal balance, DateOnly asOf) => balance + Amount;
}
