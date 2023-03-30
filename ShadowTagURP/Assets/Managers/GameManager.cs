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
    private LiftGammaGain liftGammaGain;
    [Header("Audio Values")]
    public float masterVolume;
    public float musicVolume;
    public float effectVolume;
    public float ambienceVolume;
    public float dialougeVolume;
    [Header("Menu")]
    [SerializeField] private GameObject ingameMenu;
    public bool switcher = false;
    public bool inMenu;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null || Instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GetSensitivity();
        GetVolume();
        UpdateVolumeGamma();
        ingameMenu.SetActive(false);
    }
    public void GetSensitivity()
    {
        cFOV = (int)Settings.Instance.cFOV.value;
        gamma = (int)Settings.Instance.gamma.value;
        xSliderValue = Settings.Instance.xSlider.value;
        ySliderValue = Settings.Instance.ySlider.value;
        xCSliderValue = Settings.Instance.xCSlider.value;
        yCSliderValue = Settings.Instance.yCSlider.value;
    }
    public void GetVolume()
    {
        masterVolume = Settings.Instance.masterVolume.value;;
        musicVolume = Settings.Instance.musicVolume.value;
        effectVolume = Settings.Instance.effectVolume.value;
        ambienceVolume = Settings.Instance.ambienceVolume.value;
        dialougeVolume = Settings.Instance.dialougeVolume.value;
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
    private void UpdateVolumeGamma()
    {
        GameObject volumeComponentObj;
        if (volumeComponentObj = GameObject.FindGameObjectWithTag("Volume"))
        {
            volumeComponentObj.TryGetComponent<Volume>(out Volume volume);
            volume.profile.TryGet(out liftGammaGain);
            liftGammaGain.gamma.value = new Vector4(gamma, gamma, gamma, gamma);
        }
    }
}
