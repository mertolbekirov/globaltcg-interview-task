using System.Collections.Concurrent;

namespace Marketplace.Tests;

public class Step5_ConcurrencyTests
{
    // Step 5 — discussion + (optional) live coding.
    // Un-skip this to show that WalletBook is not safe under concurrent use. Debit does a
    // check-then-act (read balance, compare, write) with no synchronisation, so concurrent
    // debits race: they can overdraw the wallet or lose updates. The goal is the conversation
    // (lock vs. ConcurrentDictionary vs. optimistic concurrency / RowVersion + retry, and how
    // this changes once the wallet lives in a shared store across several API instances),
    // not a green tick.
    [Fact(Skip = "Step 5 discussion — WalletBook is not thread-safe. See TASK.md.")]
    public void Debits_DoNotOverdrawUnderConcurrentLoad()
    {
        var wallets = new WalletBook();
        wallets.Credit("alice", "EUR", 1000m);

        var errors = new ConcurrentQueue<Exception>();

        // 1000 concurrent debits of 1 each: a correct wallet ends at exactly 0 and never throws.
        var tasks = Enumerable.Range(0, 1000).Select(_ => Task.Run(() =>
        {
            try { wallets.Debit("alice", "EUR", 1m); }
            catch (Exception ex) { errors.Enqueue(ex); }
        }));

        Task.WaitAll(tasks.ToArray());

        Assert.Empty(errors);
        Assert.Equal(0m, wallets.GetBalance("alice", "EUR"));
    }
}
