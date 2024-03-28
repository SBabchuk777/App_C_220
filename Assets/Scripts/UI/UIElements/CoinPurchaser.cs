using Slots.Game.Values;
using UnityEngine;


public class CoinPurchaser : MonoBehaviour
{
    public void AddCoins(float amount)
    {
        Wallet.AddMoney((int) amount);
    }
}
