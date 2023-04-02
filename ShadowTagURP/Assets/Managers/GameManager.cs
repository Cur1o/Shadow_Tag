using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
    [SerializeField] private Vector4 controuls;
    [Header("Menu")]
    [SerializeField] private GameObject ingameMenu;
    [SerializeField] private GameObject playerUi;
    public bool switcher = false;
    public bool inMenu;
    [Header("Cinema Camera")]
    public Camera cinemaCamera;
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
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        if (!switcher)
        {
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
}
