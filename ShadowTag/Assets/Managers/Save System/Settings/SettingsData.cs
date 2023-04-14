using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSettings
{
    [Header("Settings")]
    public float xSlider;
    public float ySlider;
    public float xCSlider;
    public float yCSlider;
    public float cFOV;
    public float gamma;
    public float masterVolume;
    public float musicVolume;
    public float effectVolume;
    public float ambienceVolume;
    public float dialougeVolume;
    /// <summary>
    /// Te default Values for the Variables
    /// </summary>
    public SaveSettings()
    {
        this.xSlider = 30f;
        this.ySlider = 30f;
        this.xCSlider = 160f;
        this.yCSlider = 160f;
        this.cFOV = 60f;
        this.gamma = 0f;
        this.masterVolume = 0f;
        this.musicVolume = -10f;
        this.effectVolume = -10f;
        this.ambienceVolume = -10f;
        this.dialougeVolume = -10f;
    }
}
