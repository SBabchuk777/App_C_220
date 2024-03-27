using UnityEngine;


public class ResultScreen : PopupBase
{
    [Space]
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private ButtonBase restartButton;
    

    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        closeButton.OnClick.AddListener(CloseButton_OnClick);
        restartButton.OnClick.AddListener(RestartButton_OnClick);
    }

    
    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();
        
        closeButton.OnClick.RemoveListener(CloseButton_OnClick);
        restartButton.OnClick.RemoveListener(RestartButton_OnClick);
    }


    private void CloseButton_OnClick()
    {
        UIManager.Instance.HidePopup(PopupType.GameScore);
        UIManager.Instance.ShowPopup(PopupType.Menu);
        
        Hide(true);
    }


    private void RestartButton_OnClick()
    {
        Hide();
    }
}