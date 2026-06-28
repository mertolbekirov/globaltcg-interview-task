namespace Marketplace;

public enum SellerType
{
    Private,      // proceeds held in escrow until the buyer confirms receipt
    Professional, // paid out directly
    Unverified    // KYC still pending — proceeds must be held until the seller is verified
}

/// <summary>
/// Pays a seller their proceeds after a sale. Professional sellers are paid directly into
/// their wallet; private sellers' proceeds are held in the platform escrow wallet until the
/// buyer confirms receipt.
/// </summary>
public class PayoutRouter
{
    public const string EscrowWalletId = "platform-escrow";
    public const string ComplianceHoldWalletId = "platform-compliance-hold";

    private readonly WalletBook _wallets;

    public PayoutRouter(WalletBook wallets) => _wallets = wallets;

    public void Pay(string sellerId, SellerType sellerType, decimal payout, string currency)
    {
        switch (sellerType)
        {
            case SellerType.Professional:
                _wallets.Credit(sellerId, currency, payout);
                break;
            case SellerType.Private:
                _wallets.Credit(EscrowWalletId, currency, payout);
                break;
            default:
                _wallets.Credit(sellerId, currency, payout);
                break;
        }
    }
}
