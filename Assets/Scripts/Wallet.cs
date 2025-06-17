using System;

public class Wallet
{
    public int Balance { get; private set; }

    public event Action<int> OnBalanceChange;

    public Wallet(int balance)
    {
        Balance = balance;
    }

    public void Deposit(int amount)
    {
        Balance += amount;

        OnBalanceChange?.Invoke(Balance);
    }

    public bool TryWithdraw(int amount)
    {
        if (amount > Balance) return false;

        Balance -= amount;

        OnBalanceChange?.Invoke(Balance);

        return true;
    }
}