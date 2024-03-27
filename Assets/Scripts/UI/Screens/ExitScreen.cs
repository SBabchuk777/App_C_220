using UnityEngine;


public class ExitScreen : PopupBase
{
    [Space]
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private ButtonBase menuButton;


    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        closeButton.OnClick.AddListener(CloseButton_OnClick);
        menuButton.OnClick.AddListener(MenuButton_OnClick);
    }

    
    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();
        
        closeButton.OnClick.RemoveListener(CloseButton_OnClick);
        menuButton.OnClick.RemoveListener(MenuButton_OnClick);
    }


    private void CloseButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.Pause);
        
        Hide();
    }


    private void MenuButton_OnClick()
    {
        UIManager.Instance.HidePopup(PopupType.Pause);
        UIManager.Instance.ShowPopup(PopupType.Menu);
        
        Hide(true);
    }
}
