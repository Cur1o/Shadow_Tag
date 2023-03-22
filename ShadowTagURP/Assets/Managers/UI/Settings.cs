using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }
    [Header("Sensitivitie Values")]
    public Slider xSlider;
    public Slider ySlider;
    public Slider xCSlider;
    public Slider yCSlider;
    [Header("Video Values")]
    public Slider cFOV;
    [Header("Audio Values")]
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider effectVolume;
    public Slider ambienceVolume;
    public Slider dialougeVolume;
    private void Awake()
    {
        Instance = this;
        xSlider.value = 30f;
        ySlider.value = 30f;
        xCSlider.value = 160f;
        yCSlider.value = 160f;
        cFOV.value = 60f;
        masterVolume.value = 0f;
        musicVolume.value = 0f;
        effectVolume.value = 0f;
        ambienceVolume.value = 0f;
        dialougeVolume.value = 0f;

    ChangeSensitivity();
        ChangeVolume();
    }
    public void ChangeSensitivity()
    {
        GameManager.Instance.GetSensitivity();
    }
    public void ChangeVolume()
    {
        GameManager.Instance.GetVolume();
    }
    public void SetWindowInactive(GameObject obj)
    {
        obj.SetActive(false);
    }

}
