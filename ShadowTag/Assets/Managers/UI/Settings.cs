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
    public Slider gamma;
    [Header("Audio Values")]
    public Slider masterVolume;
    public Slider musicVolume;
    public Slider effectVolume;
    public Slider ambienceVolume;
    public Slider dialougeVolume;
    public Button backButton;
    private void Awake()
    {
        if (Instance != null || Instance == this) Destroy(gameObject);
        else Instance = this;
        xSlider.value = 30f;
        ySlider.value = 30f;
        xCSlider.value = 160f;
        yCSlider.value = 160f;
        cFOV.value = 60f;
        masterVolume.value = 0f;
        musicVolume.value = -10f;
        effectVolume.value = -10f;
        ambienceVolume.value = -10f;
        dialougeVolume.value = -10f;
        backButton.onClick.AddListener(deactivate);
        ChangeSensitivity(); 
    }
    public void SetWindowInactive(GameObject obj)
    {
        GameManager.Instance.UpdateVolumeGamma();
        Time.timeScale = 0;
        GameManager.Instance.inMenu = false;
        obj.SetActive(false);
    }
    public void ChangeSensitivity() => GameManager.Instance.GetSensitivity();
    public void ChangeVolume() => AudioManager.Instance.SetAudio();
    public void ChangeGamma() => GameManager.Instance.UpdateVolumeGamma();
    private void deactivate() => SetWindowInactive(gameObject);
}
