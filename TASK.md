# Checkout & Wallets — Pair Programming Task

Welcome, and thanks for taking the time. This is a small C# exercise modelled on a slice of an
**online marketplace** — independent sellers list items, buyers pay, and the platform moves the
money. Everything you need is below. We care far more about how you think, read code, and talk
through trade-offs than about how many lines you write. Think out loud, ask questions, treat us as
your pair.

## The domain

The marketplace is multi-currency and money is held in wallets:

- **Buyers pay in their own currency.** A listing is priced in the *seller's* currency, so at
  checkout we convert using the current **exchange rate** plus a **2% platform spread**.
- **The platform takes a 6% fee** out of the seller's proceeds.
- **Sellers are paid differently by type.** A **Professional** seller is paid out **directly**.
  A **Private** seller's proceeds are held in **escrow** until the buyer confirms receipt, then released.
- **Wallets hold balances, one per user per currency** — your EUR wallet and your USD wallet are
  separate things.

## How to run

```bash
dotnet test                       # everything
dotnet test --filter Step1        # just the step you're on (Step1 … Step5)
```

Some tests pass. **Some fail.** A failing test describes behaviour the system is supposed to have
but currently doesn't.

## The steps — please do them in order

Each step has its own test file (`tests/Marketplace.Tests/StepN_*.cs`). You won't necessarily
finish all five — that's fine.

### Step 1 — Checkout pricing
*Code:* `ExchangeRateBook.cs`, `CheckoutCalculator.cs` · *Tests:* `Step1_CheckoutPricingTests`

- Some pricing tests fail. Work out why and make them pass.
- Take a hard look at how `ExchangeRateBook` stores rates and how `GetRate` finds one. Is this a
  sensible way to hold this data? If not, change it — and tell us why.

### Step 2 — Wallets
*Code:* `WalletBook.cs` · *Tests:* `Step2_WalletTests`

- A wallet test fails. Find the cause and fix it.

### Step 3 — Escrow release
*Code:* `LedgerEntry.cs`, `Deposit.cs`, `EscrowRelease.cs` · *Tests:* `Step3_EscrowReleaseTests`

- A private seller's escrowed funds become available **on or after** the buyer's confirmation date.
  Implement the stubbed `EscrowRelease.ApplyTo` so its tests pass.

### Step 4 — A new kind of seller
*Code:* `PayoutRouter.cs` · *Tests:* `Step4_PayoutRoutingTests`

- We've started taking on sellers whose identity isn't verified yet (KYC still pending). Their sale
  proceeds must be **held for compliance** — not paid out — until they're verified. A test describes
  the expected behaviour; make it pass.

### Step 5 — Concurrency *(discussion)*
*Code:* `WalletBook.cs` · *Tests:* `Step5_ConcurrencyTests` (skipped — un-skip it)

- In production, money moves under load: many requests crediting and debiting the same wallet at
  once. Is `WalletBook` safe? Un-skip the test to see. How would you make it safe, and what are the
  trade-offs?

Have fun. Talk us through your reasoning.
