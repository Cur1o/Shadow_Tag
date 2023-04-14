using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Settings : MonoBehaviour, IDataPersistanceSettings
{
    public static Settings Instance { get; private set; }
    public bool settingsSaved;
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
    public Button saveButton;
    public Button resetButton;
    private void Awake()
    {
        if (Instance != null || Instance == this) Destroy(gameObject);
        else Instance = this;
        SaveManager.Instance.dataPersistenceObjectsSettings.Add(this);
        LoadSettings(SaveManager.Instance.settingsData);
        backButton.onClick.AddListener(deactivate);
        saveButton.onClick.AddListener(SaveManager.Instance.SaveSettings);
        resetButton.onClick.AddListener(SaveManager.Instance.NewSettings);
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

    public void SaveSettings(ref SaveSettings settings) 
    {
        settings.xSlider = this.xSlider.value;
        settings.ySlider = this.ySlider.value;
        settings.xCSlider = this.xCSlider.value;
        settings.yCSlider = this.yCSlider.value;
        settings.cFOV = this.cFOV.value;
        settings.gamma = this.gamma.value;
        settings.masterVolume = this.masterVolume.value;
        settings.musicVolume = this.musicVolume.value;
        settings.effectVolume = this.effectVolume.value;
        settings.ambienceVolume = this.ambienceVolume.value;
        settings.dialougeVolume = this.dialougeVolume.value;  
    }
    public void LoadSettings(SaveSettings settings)
    {
        this.xSlider.value = settings.xSlider;
        this.ySlider.value = settings.ySlider;
        this.xCSlider.value = settings.xCSlider;
        this.yCSlider.value = settings.yCSlider;
        this.cFOV.value = settings.cFOV;
        this.gamma.value = settings.gamma;
        this.masterVolume.value = settings.masterVolume;
        this.musicVolume.value = settings.musicVolume;
        this.effectVolume.value = settings.effectVolume;
        this.ambienceVolume.value = settings.ambienceVolume;
        this.dialougeVolume.value = settings.dialougeVolume;
    }
}
