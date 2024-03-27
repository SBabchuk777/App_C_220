using UnityEngine;


public class ProgressBar : MonoBehaviour
{
    [SerializeField] private float minWidth;
    [SerializeField] private float maxWidth;
    [SerializeField] private RectTransform fillTransform;


    private float _currentValue = 1f;
    

    private void Awake()
    {
    }


    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        
        if (!Mathf.Approximately(_currentValue, value))
        {
            _currentValue = value;
            
            UpdateVisuals();
        }
    }


    private void UpdateVisuals()
    {
        float height = fillTransform.sizeDelta.y;
        float width = Mathf.Lerp(minWidth, maxWidth, _currentValue);
        bool isZero = Mathf.Approximately(_currentValue, 0f);
        
        fillTransform.gameObject.SetActive(!isZero);
        fillTransform.sizeDelta = new Vector2(width, height);
    }
}
