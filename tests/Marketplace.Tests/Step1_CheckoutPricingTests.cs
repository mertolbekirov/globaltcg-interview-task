namespace Marketplace.Tests;

public class Step1_CheckoutPricingTests
{
    [Fact]
    public void BuyerCharge_SameCurrency_AddsOnlyTheSpread()
    {
        var calc = new CheckoutCalculator(new ExchangeRateBook());

        // 100 EUR listing, buyer also pays in EUR: no FX, just the 2% spread.
        Assert.Equal(102m, calc.BuyerCharge(100m, "EUR", "EUR"));
    }

    [Fact]
    public void BuyerCharge_ConvertsAtRatePlusSpread()
    {
        var rates = new ExchangeRateBook();
        rates.SetRate("EUR", "USD", 1.10m);
        var calc = new CheckoutCalculator(rates);

        // 100 EUR × 1.10 × 1.02
        Assert.Equal(112.2m, calc.BuyerCharge(100m, "EUR", "USD"));
    }

    [Fact]
    public void PlatformFee_IsSixPercentOfListing()
    {
        var calc = new CheckoutCalculator(new ExchangeRateBook());

        Assert.Equal(6m, calc.PlatformFee(100m));
    }

    [Fact]
    public void SellerPayout_IsListingMinusFee()
    {
        var calc = new CheckoutCalculator(new ExchangeRateBook());

        Assert.Equal(94m, calc.SellerPayout(100m));
    }

    [Fact]
    public void BuyerCharge_WorksAcrossMultipleCurrencyPairs()
    {
        var rates = new ExchangeRateBook();
        rates.SetRate("EUR", "USD", 1.10m);
        rates.SetRate("EUR", "GBP", 0.85m);
        var calc = new CheckoutCalculator(rates);

        Assert.Equal(112.2m, calc.BuyerCharge(100m, "EUR", "USD")); // 100 × 1.10 × 1.02
        Assert.Equal(86.7m, calc.BuyerCharge(100m, "EUR", "GBP"));  // 100 × 0.85 × 1.02
    }

    [Fact]
    public void GetRate_ReturnsTheMostRecentRateForAPair()
    {
        var rates = new ExchangeRateBook();
        rates.SetRate("EUR", "USD", 1.10m);
        rates.SetRate("EUR", "USD", 1.12m); // the rate moved

        Assert.Equal(1.12m, rates.GetRate("EUR", "USD"));
    }
}
