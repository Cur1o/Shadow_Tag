using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class UIMainMenu : MonoBehaviour
{
    public static UIMainMenu Instance { get; private set; }
    [Header("Menu Buttons")]
    [SerializeField] Button _popupWarning;
    [SerializeField] Button _closePopup;
    [SerializeField] Button _newGame;
    [SerializeField] Button _loadGame;
    [SerializeField] Button _settings;
    [SerializeField] Button _exitGame;
    [SerializeField] Button _credits;
    [Header("Menu")]
    [SerializeField] private GameObject ingameMenu;
    bool switcher = false;
    [Header("Settings")]
    [SerializeField] private GameObject settings;
    [Header("Credits")]
    [SerializeField] private GameObject credits;
    [Header("Popup OBJ")]
    [SerializeField] private GameObject popupWindow;
    bool isPopup = false;
    private void Awake()
    {
        if (Instance != null || Instance == this) Destroy(gameObject);
        else Instance = this;
    }
    void Start()
    {
        _popupWarning.onClick.AddListener(SchowPopup);
        _closePopup.onClick.AddListener(SchowPopup);
        _newGame.onClick.AddListener(StartNewGame);
        _loadGame.onClick.AddListener(LoadGame);
        _settings.onClick.AddListener(SettingsUI);
        _exitGame.onClick.AddListener(Exit);
        _credits.onClick.AddListener(Credits);
        AudioManager.Instance.SetAudio();
        ingameMenu.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
        popupWindow.SetActive(false);

    }
    private void StartNewGame()
    {
        SaveManager.Instance.gameData = null;
        SaveManager.Instance.SaveGame();
        SaveManager.Instance.LoadGame();
        PlayerUI.Instance.LoadData(SaveManager.Instance.gameData);
        PlayerUI.Instance.UpdateScore(SaveManager.Instance.gameData.currentPoints);
        PlayerUI.Instance.UpdateAmmunition(0, 0);
        PlayerUI.Instance.UpdateLevel();
        ScenesManager.Instance.LoadNewGame();
        gameObject.SetActive(false);
        SchowPopup();
    }
    private void SchowPopup()
    {
        isPopup = !isPopup;
        popupWindow.SetActive(isPopup);
    }
    private void LoadGame()
    {
        SaveManager.Instance.LoadGame();
        ScenesManager.Scenes currentScene = (ScenesManager.Scenes)(SaveManager.Instance.gameData.currentLabyrinthLevel);
        if (currentScene == ScenesManager.Scenes.Menu) currentScene = ScenesManager.Scenes.Start;
        ScenesManager.Instance.LoadScene(currentScene);
        PlayerUI.Instance.UpdateLevel();
        if (switcher) Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
    public void SettingsUI()
    {
        Time.timeScale = 1f;
        GameManager.Instance.inMenu = true;
        settings.SetActive(true);
    }
    private void Exit() => Application.Quit();
    public void Credits()
    {
        Time.timeScale = 1f;
        GameManager.Instance.inMenu = true;
        credits.SetActive(true);
    }
}
