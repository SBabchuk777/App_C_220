using UnityEngine;


public class MenuScreen : PopupBase
{
    [Space]
    [SerializeField] private ButtonBase dailyButton;
    [SerializeField] private ButtonBase shopButton;
    [SerializeField] private ButtonBase settingsButton;
    [SerializeField] private ButtonBase playButton;


    public void SetRewardButton(bool isEnabled)
    {
        dailyButton.SetInteractable(isEnabled);
    }


    protected override void BeforeShow()
    {
        base.BeforeShow();

        dailyButton.SetInteractable(SkinManager.Instance.IsRewardAvailable());
    }


    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        dailyButton.OnClick.AddListener(DailyButton_OnClick);
        shopButton.OnClick.AddListener(ShopButton_OnClick);
        settingsButton.OnClick.AddListener(SettingsButton_OnClick);
        playButton.OnClick.AddListener(PlayButton_OnClick);
    }

    
    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();
        
        dailyButton.OnClick.RemoveListener(DailyButton_OnClick);
        shopButton.OnClick.RemoveListener(ShopButton_OnClick);
        settingsButton.OnClick.RemoveListener(SettingsButton_OnClick);
        playButton.OnClick.RemoveListener(PlayButton_OnClick);
    }


    private void DailyButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.Gift);
        
        Hide();
    }


    private void ShopButton_OnClick()
    {
        UIManager.Instance.ShowShop(false);
    }


    private void SettingsButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.Settings);
    }


    private void PlayButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.GameScore);
        
        Hide();
    }
}
