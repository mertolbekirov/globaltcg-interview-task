namespace Marketplace;

/// <summary>
/// Works out, for a single listing, what the buyer is charged (in their own currency) and
/// what the seller earns. Buyers pay in their preferred currency; a listing is priced in the
/// seller's currency, so we convert at the current rate plus a platform spread. The platform
/// also keeps a flat fee out of the seller's proceeds.
/// </summary>
public class CheckoutCalculator
{
    public const decimal FxSpread = 0.02m;           // 2% added on top of the raw FX rate
    public const decimal PlatformFeePercent = 0.06m; // 6% of the listing price

    private readonly ExchangeRateBook _rates;

    public CheckoutCalculator(ExchangeRateBook rates) => _rates = rates;

    /// <summary>What the buyer pays, in the buyer's currency.</summary>
    public decimal BuyerCharge(decimal listingPrice, string listingCurrency, string buyerCurrency)
    {
        var rate = _rates.GetRate(listingCurrency, buyerCurrency);
        return listingPrice * rate * (1m + FxSpread);
    }

    /// <summary>The platform's cut, in the listing (seller's) currency.</summary>
    public decimal PlatformFee(decimal listingPrice) => listingPrice * PlatformFeePercent;

    /// <summary>What the seller earns after the platform fee, in the listing currency.</summary>
    public decimal SellerPayout(decimal listingPrice) => listingPrice - PlatformFee(listingPrice);
}
