using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Button _newGame;
    void Start()
    {
        _newGame.onClick.AddListener(StartNewGame);
    }
    private void StartNewGame()
    {
        ScenesManager.Instance.LoadNewGame();
    }
}
//ScenesManager.Instance.LoadScene(ScenesManager.Scene.Map1); to load a scene from outside
