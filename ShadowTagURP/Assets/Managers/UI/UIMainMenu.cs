using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class UIMainMenu : MonoBehaviour
{
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
    [SerializeField] private GameObject options;
    [Header("Credits")]
    [SerializeField] private GameObject credits;
    void Start()
    {
        _newGame.onClick.AddListener(StartNewGame);
        _loadGame.onClick.AddListener(LoadGame);
        _settings.onClick.AddListener(Settings);
        _exitGame.onClick.AddListener(Exit);
        _credits.onClick.AddListener(Credits);
    }
    private void StartNewGame()
    {
        SaveManager.Instance.NewGame();
        ScenesManager.Instance.LoadNewGame();
    }
    private void LoadGame()
    {
        SaveManager.Instance.LoadGame();
        ScenesManager.Scene currentScene = (ScenesManager.Scene)(SaveManager.Instance.gameData.currentLabyrinthLevel + 1);
        ScenesManager.Instance.LoadScene(currentScene);
    }
    private void Settings()
    {

    }
    private void Exit()
    {
        Application.Quit();
    }
    private void Credits()
    {

    }
    public void LoadIngameMenu()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame)
        {
            switcher = !switcher;
            ingameMenu.SetActive(switcher);
        }
    }
}
//ScenesManager.Instance.LoadScene(ScenesManager.Scene.Map1); to load a scene from outside
