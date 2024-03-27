using UnityEngine;


public class GameScreen : PopupBase
{
    [Space]
    [SerializeField] private ButtonBase pauseButton;


    public void SetContent(bool isVisible)
    {
        root.gameObject.SetActive(isVisible);
    }


    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        pauseButton.OnClick.AddListener(PauseButton_OnClick);
    }

    
    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();
        
        pauseButton.OnClick.RemoveListener(PauseButton_OnClick);
    }
    

    private void PauseButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.Pause);
    }
}
