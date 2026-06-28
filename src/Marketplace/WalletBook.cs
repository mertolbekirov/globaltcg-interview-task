namespace Marketplace;

/// <summary>
/// Holds every user's wallet balances. A user has one balance per currency — their EUR
/// wallet is separate from their USD wallet.
/// </summary>
public class WalletBook
{
    private readonly Dictionary<string, decimal> _balances = new();

    /// <summary>Adds funds to a user's wallet for the given currency.</summary>
    public void Credit(string userId, string currency, decimal amount)
    {
        _balances.TryGetValue(userId, out var current);
        _balances[userId] = current + amount;
    }

    /// <summary>Removes funds from a user's wallet; throws if the balance is too low.</summary>
    public void Debit(string userId, string currency, decimal amount)
    {
        _balances.TryGetValue(userId, out var current);

        if (current < amount)
        {
            throw new InvalidOperationException($"Insufficient {currency} funds for {userId}.");
        }

        _balances[userId] = current - amount;
    }

    /// <summary>Current balance for a user in the given currency.</summary>
    public decimal GetBalance(string userId, string currency)
    {
        _balances.TryGetValue(userId, out var current);
        return current;
    }
}
