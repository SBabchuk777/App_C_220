using UnityEngine;


public class ShopScreen : PopupBase
{
    [Space]
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private GameObject character;
    [SerializeField] private RectTransform body;
    [SerializeField] private float topPosition;
    [SerializeField] private float botPosition;

    private bool _isCharacter;


    public void SetCharacter(bool isVisible)
    {
        _isCharacter = isVisible;
    }


    protected override void BeforeShow()
    {
        base.BeforeShow();
        
        var bodyPositionY = _isCharacter
            ? topPosition
            : botPosition;

        body.anchoredPosition = new Vector2(0f, bodyPositionY);
        character.SetActive(_isCharacter);
    }


    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        closeButton.OnClick.AddListener(CloseButton_OnClick);
    }


    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();

        closeButton.OnClick.RemoveListener(CloseButton_OnClick);
    }


    private void CloseButton_OnClick()
    {
        Hide();
    }
}
