using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Button _newGame;
    [SerializeField] Button _loadGame;
    void Start()
    {
        _newGame.onClick.AddListener(StartNewGame);
        _loadGame.onClick.AddListener(LoadGame);
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
}
//ScenesManager.Instance.LoadScene(ScenesManager.Scene.Map1); to load a scene from outside
