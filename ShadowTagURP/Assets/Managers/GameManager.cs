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

        ingameMenu.SetActive(false);
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
        Debug.Log("Gamma = "+gamma);
        controuls.w = gamma;
        Debug.Log("Vector4 = "+controuls.w);
        currentVolumeObj = GameObject.FindWithTag("Volume");
        if (currentVolumeObj != null)
        {
            currentVolumeObj.TryGetComponent<Volume>(out currentVolume);
            currentVolume.profile.TryGet(out LiftGammaGain liftGammaGain);
            //Debug.Log(liftGammaGain);
            liftGammaGain.gamma.value = controuls;
        }
    }
}
