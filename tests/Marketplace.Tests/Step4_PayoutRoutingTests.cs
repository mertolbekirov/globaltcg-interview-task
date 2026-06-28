namespace Marketplace.Tests;

public class Step4_PayoutRoutingTests
{
    [Fact]
    public void ProfessionalSeller_IsPaidDirectly()
    {
        var wallets = new WalletBook();
        var router = new PayoutRouter(wallets);

        router.Pay("seller-1", SellerType.Professional, 94m, "EUR");

        Assert.Equal(94m, wallets.GetBalance("seller-1", "EUR"));
        Assert.Equal(0m, wallets.GetBalance(PayoutRouter.EscrowWalletId, "EUR"));
    }

    [Fact]
    public void PrivateSeller_ProceedsGoToEscrow()
    {
        var wallets = new WalletBook();
        var router = new PayoutRouter(wallets);

        router.Pay("seller-1", SellerType.Private, 94m, "EUR");

        Assert.Equal(0m, wallets.GetBalance("seller-1", "EUR"));
        Assert.Equal(94m, wallets.GetBalance(PayoutRouter.EscrowWalletId, "EUR"));
    }

    [Fact]
    public void UnverifiedSeller_ProceedsAreHeldForCompliance()
    {
        var wallets = new WalletBook();
        var router = new PayoutRouter(wallets);

        router.Pay("seller-1", SellerType.Unverified, 94m, "EUR");

        // An unverified seller must NOT be paid — their proceeds are held until KYC clears.
        Assert.Equal(0m, wallets.GetBalance("seller-1", "EUR"));
        Assert.Equal(94m, wallets.GetBalance(PayoutRouter.ComplianceHoldWalletId, "EUR"));
    }
}
