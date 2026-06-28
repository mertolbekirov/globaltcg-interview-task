namespace Marketplace.Tests;

public class Step2_WalletTests
{
    [Fact]
    public void Credit_ThenGetBalance_ReturnsCreditedAmount()
    {
        var wallets = new WalletBook();
        wallets.Credit("alice", "EUR", 50m);

        Assert.Equal(50m, wallets.GetBalance("alice", "EUR"));
    }

    [Fact]
    public void Debit_ReducesBalance()
    {
        var wallets = new WalletBook();
        wallets.Credit("alice", "EUR", 50m);
        wallets.Debit("alice", "EUR", 20m);

        Assert.Equal(30m, wallets.GetBalance("alice", "EUR"));
    }

    [Fact]
    public void Debit_BeyondBalance_Throws()
    {
        var wallets = new WalletBook();
        wallets.Credit("alice", "EUR", 10m);

        Assert.Throws<InvalidOperationException>(() => wallets.Debit("alice", "EUR", 20m));
    }

    [Fact]
    public void Balances_AreSeparatePerCurrency()
    {
        var wallets = new WalletBook();
        wallets.Credit("alice", "EUR", 50m);
        wallets.Credit("alice", "USD", 30m);

        // Alice's EUR and USD wallets are independent.
        Assert.Equal(50m, wallets.GetBalance("alice", "EUR"));
        Assert.Equal(30m, wallets.GetBalance("alice", "USD"));
    }
}
