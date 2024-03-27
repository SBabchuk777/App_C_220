using UnityEngine;


public class SwitchButton : ButtonBase
{
    [SerializeField] private GameObject onImage;
    [SerializeField] private GameObject offImage;

    private bool _isActive;


    public bool IsActive => _isActive;
    
    
    public void SwitchState()
    {
        _isActive = !_isActive;

        UpdateVisuals();
    }


    public void SetState(bool isActive)
    {
        _isActive = isActive;

        UpdateVisuals();
    }


    private void UpdateVisuals()
    {
        onImage.SetActive(_isActive);
        offImage.SetActive(!_isActive);
    }
}
