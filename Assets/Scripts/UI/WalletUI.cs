using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balanceTMP;

    private void OnDisable()
    {
        GameManager.Instance.Wallet.OnBalanceChange -= UpdateBalance;
    }

    private void Start()
    {
        GameManager.Instance.Wallet.OnBalanceChange += UpdateBalance;

        UpdateBalance(GameManager.Instance.Wallet.Balance);
    }

    private void UpdateBalance(int balance)
    {
        _balanceTMP.SetText($"Balance: {Utils.BalanceFormatter(balance)}");
    }
}