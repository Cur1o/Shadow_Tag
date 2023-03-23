using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [Header("Audio Values")]
    public float masterVolume;
    public float musicVolume;
    public float effectVolume;
    public float ambienceVolume;
    public float dialougeVolume;
    [Header("Menu")]
    [SerializeField] private GameObject ingameMenu;
    public bool switcher = false;
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
        switcher = !switcher;
        if(switcher)Cursor.lockState = CursorLockMode.None;
        if(!switcher)Cursor.lockState = CursorLockMode.Locked;
        ingameMenu.SetActive(switcher);
    }
}
