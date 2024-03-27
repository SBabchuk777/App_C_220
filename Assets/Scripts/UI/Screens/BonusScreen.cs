using System;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UnityEngine;


public class BonusScreen : PopupBase
{
    [Space]
    [SerializeField] private ButtonBase closeButton;
    [SerializeField] private ButtonBase spinButton;
    [SerializeField] private StateObject rewardStates;
    [SerializeField] private List<SlotRow> slotRows;

    private float _currentRewardAmount;
    private Tween _dialogTween;


    protected override void BeforeShow()
    {
        base.BeforeShow();

        closeButton.Hide();
        spinButton.Show();
        spinButton.SetInteractable(true);
        rewardStates.SetState(0);

        _currentRewardAmount = 100f;

        GetRewardAmount();
    }


    protected override void AfterHide()
    {
        _dialogTween?.Kill();
        
        base.AfterHide();
    }


    private void Spin()
    {
        for (int i = 0; i < slotRows.Count; i++)
        {
            var currentIndex = i;
            
            Action callback = currentIndex == slotRows.Count - 1
                ? CollectReward
                : null;

            var rowSpinAction = new Action<long>(_ =>
            {
                slotRows[currentIndex].Spin(callback);
            });
            
            Observable.Timer(TimeSpan.FromSeconds(.3f * currentIndex))
                .Subscribe(rowSpinAction).AddTo(this);
        }
    }


    protected override void SubscribeButtons()
    {
        base.SubscribeButtons();
        
        closeButton.OnClick.AddListener(CloseButton_OnClick);
        spinButton.OnClick.AddListener(SpinButton_OnClick);
    }


    protected override void UnSubscribeButtons()
    {
        base.UnSubscribeButtons();
        
        closeButton.OnClick.RemoveListener(CloseButton_OnClick);
        spinButton.OnClick.RemoveListener(SpinButton_OnClick);
    }


    private void CollectReward()
    {
        spinButton.Hide();
        closeButton.Show();
        
        if (slotRows[0].ResultIndex == slotRows[1].ResultIndex &&
            slotRows[1].ResultIndex == slotRows[2].ResultIndex)
        {
            AudioManager.Instance.PlaySound(AudioClipType.Win);

            rewardStates.SetState(1);
            
            CurrencyManager.Instance.AddCurrency(CurrencyType.Coins, _currentRewardAmount);
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioClipType.Lose);
            
            rewardStates.SetState(2);
        }
        
        SkinManager.Instance.ClaimReward();
    }


    private void GetRewardAmount()
    {
        _currentRewardAmount = 100f;
    }


    private void CloseButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.Menu);
        
        Hide();
    }


    private void SpinButton_OnClick()
    {
        spinButton.SetInteractable(false);
        
        Spin();
    }
}