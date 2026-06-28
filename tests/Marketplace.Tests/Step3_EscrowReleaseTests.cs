namespace Marketplace.Tests;

public class Step3_EscrowReleaseTests
{
    [Fact]
    public void Deposit_AppliesImmediately()
    {
        LedgerEntry deposit = new Deposit(50m);

        Assert.Equal(60m, deposit.ApplyTo(10m, new DateOnly(2026, 1, 1)));
    }

    [Fact]
    public void EscrowRelease_BeforeReleaseDate_HasNoEffect()
    {
        LedgerEntry release = new EscrowRelease(94m, releaseOn: new DateOnly(2026, 7, 1));

        Assert.Equal(0m, release.ApplyTo(0m, asOf: new DateOnly(2026, 6, 20)));
    }

    [Fact]
    public void EscrowRelease_OnOrAfterReleaseDate_AddsTheEscrowedAmount()
    {
        LedgerEntry release = new EscrowRelease(94m, releaseOn: new DateOnly(2026, 7, 1));

        Assert.Equal(94m, release.ApplyTo(0m, asOf: new DateOnly(2026, 7, 1)));
    }
}
