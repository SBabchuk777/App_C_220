using System;

namespace Slots.Game.Values
{
    public static class Wallet
    {
        public static event Action<int> OnChangedMoney = null;

        public static int Money
        {
            // get => PlayerPrefs.GetInt("WalletMoney", 0);
            get => (int) CurrencyManager.Instance.GetCurrencyAmount(CurrencyType.Coins);

            private set
            {
                if (value > 999999999)
                    value = 999999999;

                CurrencyManager.Instance.SetCurrency(CurrencyType.Coins, value);
                
                // PlayerPrefs.SetInt("WalletMoney", value);
                // PlayerPrefs.Save();

                OnChangedMoney?.Invoke(value);
            }
        }

        public static void AddMoney(int money)
        {
            if (money > 0)
                Money += money;
        }

        public static bool TryPurchase(int money)
        {
            if (Money >= money)
            {
                Money -= money;

                return true;
            }

            return false;
        }
    }
}