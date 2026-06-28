namespace Marketplace;

/// <summary>A single recorded exchange-rate update.</summary>
public record RateChange(string From, string To, decimal Rate);

/// <summary>
/// Stores exchange rates as an append-only list of updates; the current rate for a pair is the
/// one most recently recorded for it. Rates are directional: <c>GetRate("EUR", "USD")</c> is how
/// many USD one EUR buys.
/// </summary>
public class ExchangeRateBook
{
    private readonly List<RateChange> _changes = new();

    /// <summary>Records a new rate for a currency pair.</summary>
    public void SetRate(string from, string to, decimal rate)
        => _changes.Add(new RateChange(from, to, rate));

    /// <summary>The current rate for converting <paramref name="from"/> into <paramref name="to"/>.</summary>
    public decimal GetRate(string from, string to)
    {
        if (from == to)
        {
            return 1m;
        }

        var change = _changes.FirstOrDefault(c => c.From == from && c.To == to);

        if (change is null)
        {
            throw new KeyNotFoundException($"No rate recorded for {from}->{to}.");
        }

        return change.Rate;
    }
}
