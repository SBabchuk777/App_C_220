using UnityEngine;


public class PauseScreen : PopupBase
{
    [Space]
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private ButtonBase restartButton;
    [SerializeField] private ButtonBase menuButton;


    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        closeButton.OnClick.AddListener(CloseButton_OnClick);
        restartButton.OnClick.AddListener(RestartButton_OnClick);
        menuButton.OnClick.AddListener(MenuButton_OnClick);
    }

    
    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();
        
        closeButton.OnClick.RemoveListener(CloseButton_OnClick);
        restartButton.OnClick.RemoveListener(RestartButton_OnClick);
        menuButton.OnClick.RemoveListener(MenuButton_OnClick);
    }


    private void CloseButton_OnClick()
    {
        Hide();
    }
    
    
    private void RestartButton_OnClick()
    {
        Hide();
    }


    private void MenuButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.Exit);
        
        Hide();
    }
}
