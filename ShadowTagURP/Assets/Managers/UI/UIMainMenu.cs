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
    private Credits scriptCredits;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        _newGame.onClick.AddListener(StartNewGame);
        _loadGame.onClick.AddListener(LoadGame);
        _settings.onClick.AddListener(Settings);
        _exitGame.onClick.AddListener(Exit);
        _credits.onClick.AddListener(Credits);
        scriptCredits = credits.GetComponent<Credits>();
        ingameMenu.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
    }
    private void StartNewGame()
    {
        SaveManager.Instance.NewGame();
        ScenesManager.Instance.LoadNewGame();
        gameObject.SetActive(false);
    }
    private void LoadGame()
    {
        SaveManager.Instance.LoadGame();
        ScenesManager.Scenes currentScene = (ScenesManager.Scenes)(SaveManager.Instance.gameData.currentLabyrinthLevel + 1);
        ScenesManager.Instance.LoadScene(currentScene);
        PlayerUI.Instance.UpdateLevel();
        if (switcher) Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }
    public void Settings()
    {
        Time.timeScale = 0.1f;
        GameManager.Instance.inMenu = true;
        settings.SetActive(true);
    }
    private void Exit()
    {
        Application.Quit();
    }
    public void Credits()
    {
        Time.timeScale = 1;
        GameManager.Instance.inMenu = true;
        credits.SetActive(true);
        scriptCredits.StartAnimation();
    }
    //public void LoadIngameMenu()
    //{
    //        switcher = !switcher;
    //        ingameMenu.SetActive(switcher);
    //}
}
