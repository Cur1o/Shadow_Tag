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
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GetSensitivity();
        GetVolume();
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
}
