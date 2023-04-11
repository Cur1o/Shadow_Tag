using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Sensitivitie Values")]
    public float xSliderValue;
    public float ySliderValue;
    public float xCSliderValue;
    public float yCSliderValue;
    [Header("Video Values")]
    public int cFOV;
    public float gamma;
    public GameObject currentVolumeObj;
    private Volume currentVolume;
    [SerializeField] private Vector4 controuls = new Vector4();
    [Header("Menu")]
    [SerializeField] private GameObject ingameMenu;
    [SerializeField] private GameObject playerUi;
    [HideInInspector]public bool switcher = false;
    [HideInInspector] public bool inMenu;
    [HideInInspector] public bool isPaused;
    [Header("Cinema Camera")]
    GameObject cinemaCameraObj;
    GameObject player;
    GameObject skipButtonObj;
    Button skipButton;
    
    private void Awake()
    {
        if (Instance != null || Instance == this) Destroy(gameObject);
        else Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GetSensitivity();

        ingameMenu.SetActive(false);
        playerUi.SetActive(false);
    }
    public void GetSensitivity()
    {
        cFOV = (int)Settings.Instance.cFOV.value;
        xSliderValue = Settings.Instance.xSlider.value;
        ySliderValue = Settings.Instance.ySlider.value;
        xCSliderValue = Settings.Instance.xCSlider.value;
        yCSliderValue = Settings.Instance.yCSlider.value;
    }
    public void OpenMenu()
    {
        if (inMenu)return;
        switcher = !switcher;
        if (switcher)
        {
            isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        if (!switcher)
        {
            isPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1; 
        }
        ingameMenu.SetActive(switcher);
    }
    public void UpdateVolumeGamma()
    {
        gamma = (float)Settings.Instance.gamma.value;
        controuls.w = gamma;
        currentVolumeObj = GameObject.FindWithTag("Volume");
        if (currentVolumeObj != null)
        {
            currentVolumeObj.TryGetComponent<Volume>(out currentVolume);
            currentVolume.profile.TryGet(out LiftGammaGain liftGammaGain);
            liftGammaGain.gamma.value = controuls;
        }
    }
    public void GetPlayer()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) return;
        player = GameObject.FindGameObjectWithTag("Player");
        skipButtonObj = GameObject.FindGameObjectWithTag("Skip");
        cinemaCameraObj = GameObject.FindGameObjectWithTag("CinemaCamera");
        skipButton = skipButtonObj.GetComponent<Button>();
        skipButton.onClick.AddListener(SkipIntro);
        //Debug.Log(player);
        if (SceneManager.GetActiveScene().buildIndex == 1) 
        {
            player.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void SkipIntro()
    {
        Debug.Log("Skip Intro");
        Destroy(cinemaCameraObj);
        player.SetActive(true);
        playerUi.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void playEnemySound(AudioClip sound)
    {

    }
}
