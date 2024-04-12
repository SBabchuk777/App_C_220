using Slots.Game.Values;
using UnityEngine;


public class SlotScreen : PopupBase
{
    [SerializeField] private ButtonBase shopButton;


    public void Exit()
    {
        UIManager.Instance.ShowPopup(PopupType.Exit);
    }


    public void ShowShop()
    {
        if (FreeSpins.Count > 0) return;
        
        UIManager.Instance.ShowShop(true);
    }


    public void PlayClick()
    {
        AudioManager.Instance.PlaySound(AudioClipType.Click);
    }


    public void PlaySpin()
    {
        AudioManager.Instance.PlaySound(AudioClipType.Spin);
    }
    
    
    public void PlayWin()
    {
        AudioManager.Instance.PlaySound(AudioClipType.Win);
    }
    
    
    public void PlayLose()
    {
        AudioManager.Instance.PlaySound(AudioClipType.Lose);
    }


    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        shopButton.OnClick.AddListener(ShopButton_OnClick);
    }


    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();
        
        shopButton.OnClick.RemoveListener(ShopButton_OnClick);
    }


    private void ShopButton_OnClick()
    {
        UIManager.Instance.ShowShop(true);
    }
}