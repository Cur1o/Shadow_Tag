using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider progressBar;

    private void Awake()
    {
        Instance = this;
    }


    private void LateUpdate()
    {
        ChangePlayerUI();
    }
    public enum Scene
    {
        Menu,
        Start,
        Map1,
        Map2,
        Map3,
    }
    public void LoadNewGame() => LoadScene(Scene.Start);
    public void LoadNextScene()
    {
        StartCoroutine(LoadNextSceneAsync());
    }
    public void LoadScene(Scene scene)
    {
        Debug.Log("Loaded Scene : "+scene);
        StartCoroutine(LoadSceneAsync(scene));
    }
    public IEnumerator LoadSceneAsync(Scene scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene.ToString());
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }  
    public IEnumerator LoadNextSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
    public void LoadMainMenu()
    {
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene(Scene.Menu.ToString());
    }
    private void ChangePlayerUI()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Menu")
        {
            playerUI.SetActive(true);
        }
        else
        {
            playerUI.SetActive(false);
            loadingScreen.SetActive(false);
        }
            
    }
}
