using System.Collections.Generic;
using System.Linq;
using Base;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : SingletonMonoBehaviour<UIManager>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasScaler canvasScaler;
    [SerializeField] private List<PopupBase> screens;

    private GameScreen _gameScreen;
    private MenuScreen _menuScreen;
    private bool _hasBeenLaunched;
    private bool _isLevelMode;
    private bool _isTutorial;
    

    public GameScreen GameScreen => _gameScreen;

    
    public MenuScreen MenuScreen => _menuScreen;


    public bool IsTutorial => _isTutorial;


    public CanvasScaler CanvasScaler => canvasScaler;


    protected override void Awake()
    {
        base.Awake();
        
        LoadData();
        
        _gameScreen = screens.FirstOrDefault(popup => popup.PopupType == PopupType.GameScore) as GameScreen;
        _menuScreen = screens.FirstOrDefault(popup => popup.PopupType == PopupType.Menu) as MenuScreen;
        
        ShowPopup(PopupType.Menu);
        
        // ShowPopup(PopupType.Gift);
        // HidePopup(PopupType.Gift);
    }


    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }


    public void ShowPopup(PopupType type)
    {
        var screen = screens.FirstOrDefault(popup => popup.PopupType == type);

        if (screen != null)
        {
            screen.Show();
        }
    }


    public void ShowResult(float coinsAmount, float rocketsAmount)
    {
        var screen = screens.FirstOrDefault(popup => popup.PopupType == PopupType.ResultScore) as ResultScreen;

        if (screen != null)
        {
            screen.Show();
            //screen.SetResult(coinsAmount, rocketsAmount);
        }
    }


    public void ShowShop(bool isCharacter)
    {
        var screen = screens.FirstOrDefault(popup => popup.PopupType == PopupType.Collection) as ShopScreen;

        if (screen != null)
        {
            screen.SetCharacter(isCharacter);
            screen.Show();
        }
    }


    public void HidePopup(PopupType type)
    {
        var screen = screens.FirstOrDefault(popup => popup.PopupType == type);

        if (screen != null && 
            screen.IsOpen.Value)
        {
            screen.Hide();
        }
    }


    public void ResolveTutorial()
    {
        _isTutorial = true;
        
        SaveData();
    }


    private void SaveData()
    {
        PlayerPrefs.SetInt("IsTutorial", _isTutorial ? 1 : 0);
        PlayerPrefs.Save();
    }


    private void LoadData()
    {
        _isTutorial = PlayerPrefs.GetInt("IsTutorial") == 1;
    }
}
