using System;
using UnityEngine;


public class FortuneWheelScreen : PopupBase
{
    private const string LastVisitKey = "LastWheelVisitDate";
    private const string ConsecutiveDaysKey = "ConsecutiveDays";

    [Space] 
    [SerializeField] private FortuneWheel wheel;
    [SerializeField] private GameObject label;
    [SerializeField] private RewardLabel rewardsLabel;
    [Space]
    [SerializeField] private ButtonBase spinButton;
    [SerializeField] private ButtonBase closeButton;

    private DateTime _lastVisitDate;
    private DateTime _currentDate;
    private int _consecutiveDays;


    protected override void Awake()
    {
        base.Awake();

        LoadVisitTime();
        LoadConsecutiveDays();
        
        _currentDate = DateTime.Now.Date;

        spinButton.SetInteractable(false);

        SetLabel(false);
        
        UIManager.Instance.MenuScreen.SetRewardButton(IsRewardAvailable());
    }


    private void Subscribe()
    {
        spinButton.OnClick.AddListener(SpinButton_OnClick);
        closeButton.OnClick.AddListener(CloseButton_OnClick);
    }


    private void UnSubscribe()
    {
        spinButton.OnClick.RemoveListener(SpinButton_OnClick);
        closeButton.OnClick.RemoveListener(CloseButton_OnClick);
    }


    private void SetLabel(bool isReward)
    {
        label.SetActive(!isReward);
        
        if (isReward)
        {
            spinButton.Hide();
            closeButton.Show();
            rewardsLabel.Show();
        }
        else
        {
            spinButton.Show();
            closeButton.Hide();
            rewardsLabel.Hide();
        }
    }


    private void CreateRewards()
    {
        var reward = wheel.GetRandomReward(out var angle);

        GetReward(reward);
        
        wheel.Rotate(angle, () =>
        {
            SetLabel(true);
        });
    }


    private bool IsRewardAvailable()
    {
        return _currentDate != _lastVisitDate;
    }


    private void GetReward(FortuneWheel.FortuneWheelReward reward)
    {
        rewardsLabel.SetReward(reward);

        if (reward.type == FortuneWheel.FortuneWheelRewardType.Coins)
        {
            CurrencyManager.Instance.AddCurrency(CurrencyType.Coins, reward.amount);
        }
        else if (reward.type == FortuneWheel.FortuneWheelRewardType.Rockets)
        {
            CurrencyManager.Instance.AddCurrency(CurrencyType.Rockets, reward.amount);
        }
        else
        {
            //
        }

        _lastVisitDate = _currentDate;

        UIManager.Instance.MenuScreen.SetRewardButton(false);
        
        SaveConsecutiveDays();
        SaveVisitTime();
    }


    protected override void AfterShow()
    {
        base.AfterShow();
        
        Subscribe();
        
        spinButton.SetInteractable(true);
    }


    protected override void BeforeHide()
    {
        base.BeforeHide();
        
        UnSubscribe();
    }
    
    
    private void SaveVisitTime()
    {
        var data = _currentDate.Ticks.ToString();
        
        PlayerPrefs.SetString(LastVisitKey, data);
    }


    private void SaveConsecutiveDays()
    {
        PlayerPrefs.SetInt(ConsecutiveDaysKey, _consecutiveDays);
    }


    private void LoadVisitTime()
    {
        var data = PlayerPrefs.GetString(LastVisitKey);
        
        if (!string.IsNullOrEmpty(data))
        {
            long ticks = Convert.ToInt64(data);
            
            _lastVisitDate = new DateTime(ticks);
        }
        else
        {
            _lastVisitDate = DateTime.MinValue;
        }
    }


    private void LoadConsecutiveDays()
    {
        _consecutiveDays = PlayerPrefs.GetInt(ConsecutiveDaysKey);
    }
    
    
    private void SpinButton_OnClick()
    {
        spinButton.SetInteractable(false);
        
        CreateRewards();
    }


    private void CloseButton_OnClick()
    {
        UIManager.Instance.ShowPopup(PopupType.Menu);
        
        Hide();
    }
}
