using UnityEngine;


public class CoinPurchaser : MonoBehaviour
{
    public void AddCoins(float amount)
    {
        CurrencyManager.Instance.AddCurrency(CurrencyType.Coins, amount);
    }
}
