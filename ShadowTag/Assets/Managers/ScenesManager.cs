using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public GameObject playerUI;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider progressBar;

    private void Awake()
    {
        if (Instance != null || Instance == this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private void Start()
    {
        SceneManager.sceneLoaded += ChangePlayerUI;
        loadingScreen.SetActive(false);   
    }
    public enum Scenes{ Menu, Start, Map1, Map2, Map3}
    public void LoadNewGame() => LoadScene(Scenes.Start);
    public void LoadNextScene() => StartCoroutine(LoadNextSceneAsync());
    public void LoadScene(Scenes scene)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync(scene));
    }
    public IEnumerator LoadSceneAsync(Scenes scene)
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
        GameManager.Instance.UpdateVolumeGamma();
    }  
    public IEnumerator LoadNextSceneAsync()
    {
        Time.timeScale = 1;
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progressValue;
            yield return null;
        }
        loadingScreen.SetActive(false);
        GameManager.Instance.UpdateVolumeGamma();
    }
    public void LoadMainMenu()
    {
        SaveManager.Instance.SaveGame();
        Time.timeScale = 1;
        SceneManager.LoadScene(Scenes.Menu.ToString());
    }
    public IEnumerator LoadHub()
    {
        Time.timeScale = 1;
        SaveManager.Instance.SaveGame();
        GameManager.Instance.OpenMenu();
        SceneManager.LoadScene(Scenes.Start.ToString());
        yield return new WaitForSeconds(1f);
        GameManager.Instance.SkipIntro();
    }
    public void EndGameSequence()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Scenes.Menu.ToString());
        UIMainMenu.Instance.Credits();
        Cursor.lockState = CursorLockMode.None;
        
    }
    private void ChangePlayerUI(Scene scene,LoadSceneMode mode)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "Menu")
            playerUI.SetActive(true);
        else
            playerUI.SetActive(false); 
    }
}
