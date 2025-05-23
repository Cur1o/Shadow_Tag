using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button _mainMenu;
    [SerializeField] Button _Hub;
    [SerializeField] Button _continue;
    [SerializeField] Button _SaveGame;
    [SerializeField] Button _settings;
    [SerializeField] Button _credits;
    [SerializeField] GameObject MainMenu;
    void Start()
    {
        _mainMenu.onClick.AddListener(LoadMainMenu);
        _Hub.onClick.AddListener(LoadHub);
        _continue.onClick.AddListener(CloseMenu);
        _SaveGame.onClick.AddListener(SaveGame);;
        _settings.onClick.AddListener(UIMainMenu.Instance.SettingsUI);
        _credits.onClick.AddListener(UIMainMenu.Instance.Credits);
    }
    private void LoadHub()
    {
        StartCoroutine(ScenesManager.Instance.LoadHub());
    }
    private void LoadMainMenu()
    {
        ScenesManager.Instance.LoadMainMenu();
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    private void CloseMenu() => GameManager.Instance.OpenMenu();
    private void SaveGame() => SaveManager.Instance.SaveGame();
}
