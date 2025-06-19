using System;

public class Wallet
{
    public int Balance { get; private set; }

    public event Action<int> OnBalanceChange;
    
    private event Action OnWidthDrawFailed;

    public Wallet(int balance, Action OnWidthDrawFailed)
    {
        Balance = balance;
        this.OnWidthDrawFailed = OnWidthDrawFailed;
    }

    public void Deposit(int amount)
    {
        Balance += amount;

        OnBalanceChange?.Invoke(Balance);
    }

    public bool TryWithdraw(int amount)
    {
        if (amount > Balance)
        {
            OnWidthDrawFailed?.Invoke();
            return false;
        }

        Balance -= amount;

        OnBalanceChange?.Invoke(Balance);

        return true;
    }
}