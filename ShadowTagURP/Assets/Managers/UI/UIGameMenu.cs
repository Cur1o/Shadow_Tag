using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button _mainMenu;
    [SerializeField] Button _continue;
    [SerializeField] Button _SaveGame;
    [SerializeField] Button _settings;
    [SerializeField] Button _credits;

    [SerializeField] GameObject MainMenu;
    void Start()
    {
        _mainMenu.onClick.AddListener(LoadMainMenu);
        _continue.onClick.AddListener(CloseMenu);
        _SaveGame.onClick.AddListener(SaveGame);;
        _settings.onClick.AddListener(UIMainMenu.Instance.Settings);
        _credits.onClick.AddListener(UIMainMenu.Instance.Credits);
    }
    private void LoadMainMenu()
    {
        SaveGame();
        ScenesManager.Instance.LoadMainMenu();
        MainMenu.SetActive(true);
    }
    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    private void SaveGame()
    {
        SaveManager.Instance.SaveGame();
    }
}
