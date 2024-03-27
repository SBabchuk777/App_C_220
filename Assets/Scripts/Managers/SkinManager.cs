using System;
using System.Collections.Generic;
using Base;
using UnityEngine;
using UnityEngine.Events;


public class SkinManager : SingletonMonoBehaviour<SkinManager>
{
    [Serializable]
    private class PurchaseData
    {
        public List<int> purchasedIndexes;
    }
    
    
    private const string CurrentSkinIndexKey = "CurrentSkinIndex";
    private const string PurchasedSkinsKey = "PurchasedSkins";
    private const string CurrentLevelKey = "CurrentLevel";
    private const string ConsecutiveDaysKey = "ConsecutiveDays";
    private const string LastVisitKey = "LastVisit";

    private PurchaseData _purchaseData;
    private DateTime _lastVisitDate;
    private DateTime _currentDate;
    private int _consecutiveDays;
    private int _currentSkinIndex;
    private int _currentLevelIndex;


    public int CurrentIndex => _currentSkinIndex;


    public int CurrentLevel => _currentLevelIndex;


    public UnityEvent OnSelect { get; } = new UnityEvent();
    
    
    protected override void Awake()
    {
        base.Awake();
        
        LoadData();
        
        _currentDate = DateTime.Now.Date;
    }


    private void OnDestroy()
    {
        OnSelect.RemoveAllListeners();
    }
    
    
    public bool IsRewardAvailable()
    {
        return _currentDate != _lastVisitDate;
    }


    public int GetConsecutiveDays()
    {
        if (_lastVisitDate.Day + 1 == _currentDate.Day)
        {
            return _consecutiveDays;
        }

        return 0;
    }


    public void ClaimReward()
    {
        _consecutiveDays++;

        _lastVisitDate = _currentDate;
        
        SaveData();
    }


    public void SetCurrentLevel(int index)
    {
        if (index > _currentLevelIndex)
        {
            _currentLevelIndex = index;
            
            SaveData();
        }
    }


    public void BuyPack(int packIndex)
    {
        _purchaseData.purchasedIndexes.Add(packIndex);
        
        SaveData();
    }


    public void SelectPack(int packIndex)
    {
        _currentSkinIndex = packIndex;
        
        SaveData();
        
        OnSelect?.Invoke();
    }


    public bool IsPurchased(int packIndex)
    {
        return _purchaseData.purchasedIndexes.Contains(packIndex);
    }


    private void SaveData()
    {
        string purchasedJson = JsonUtility.ToJson(_purchaseData);
        string lastVisitJson = _currentDate.Ticks.ToString();

        PlayerPrefs.SetString(PurchasedSkinsKey, purchasedJson);
        PlayerPrefs.SetString(LastVisitKey, lastVisitJson);
        PlayerPrefs.SetInt(CurrentSkinIndexKey, _currentSkinIndex);
        PlayerPrefs.SetInt(CurrentLevelKey, _currentLevelIndex);
        PlayerPrefs.SetInt(ConsecutiveDaysKey, _consecutiveDays);
        PlayerPrefs.Save();
    }


    private void LoadData()
    {
        _currentSkinIndex = PlayerPrefs.GetInt(CurrentSkinIndexKey);
        _currentLevelIndex = PlayerPrefs.GetInt(CurrentLevelKey);
        _consecutiveDays = PlayerPrefs.GetInt(ConsecutiveDaysKey);
        
        string data = PlayerPrefs.GetString(LastVisitKey);
        
        if (!string.IsNullOrEmpty(data))
        {
            long ticks = Convert.ToInt64(data);
            
            _lastVisitDate = new DateTime(ticks);
        }
        else
        {
            _lastVisitDate = DateTime.MinValue;
        }

        string jsonData = PlayerPrefs.GetString(PurchasedSkinsKey);

        if (!string.IsNullOrEmpty(jsonData))
        {
            _purchaseData = JsonUtility.FromJson<PurchaseData>(jsonData);
        }
        else
        {
            CreateDefaultData();
        }
    }
    
    
    private void CreateDefaultData()
    {
        _purchaseData = new PurchaseData()
        {
            purchasedIndexes = new List<int>{0}
        };

        _currentSkinIndex = 0;
        _currentLevelIndex = 0;
        _consecutiveDays = 0;
    }
}
