using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class SlotRow : MonoBehaviour
{
    [SerializeField] private int rotationsCount;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;
    [Space] 
    [SerializeField] private List<RectTransform> slots;
    [SerializeField] private List<Sprite> slotSprites;

    private List<Image> _slotImages = new List<Image>();
    private List<Vector2> _startPositions = new List<Vector2>();
    private List<Vector2> _previousPositions = new List<Vector2>();
    private float _rotationLength;
    private int _resultIndex;
    
    private Tween _spinDisposable;


    public int ResultIndex => _resultIndex;


    private void Awake()
    {
        Initialize();
    }


    public void Spin(Action callback = null)
    {
        var totalDistance = rotationsCount * _rotationLength;
        var offset = _rotationLength * .5f;
        
        _spinDisposable?.Kill();
        _spinDisposable = DOTween.To(() => 0f, t =>
            {
                var distanceTraveled = totalDistance * t;

                for (int i = 0; i < slots.Count - 1; i++)
                {
                    var delta = offset + distanceTraveled -_startPositions[i].y;
                    var modDelta = delta % _rotationLength;
                    var newPosition = new Vector2(0f, offset - modDelta);
                    
                    if (t < .95f &&
                        newPosition.y > _previousPositions[i].y)
                    {
                        SetRandomSkin(i);
                    }

                    _previousPositions[i] = newPosition;
                    slots[i].anchoredPosition = newPosition;
                }
            }, 1f, duration)
            .SetEase(curve)
            .OnComplete(() =>
            {
                AudioManager.Instance.PlaySound(AudioClipType.Click);
                
                callback?.Invoke();
            });
    }
    
    
    private void Initialize()
    {
        var firstSlot = slots.First().anchoredPosition;
        var lastSlot = slots.Last().anchoredPosition;
        
        _rotationLength = Vector2.Distance(firstSlot, lastSlot);

        foreach (var slot in slots)
        {
            _slotImages.Add(slot.GetComponentInChildren<Image>());
            _startPositions.Add(slot.anchoredPosition);
            _previousPositions.Add(slot.anchoredPosition);
        }
    }


    private void SetRandomSkin(int slotIndex)
    {
        var randomIndex = Random.Range(0, slotSprites.Count);

        if (slotIndex == 1)
        {
            _resultIndex = randomIndex;
        }

        _slotImages[slotIndex].sprite = slotSprites[randomIndex];
    }
}
