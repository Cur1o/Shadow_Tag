using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance { get; private set; }
    public Slider cFOV;
    public Slider xSlider;
    public Slider ySlider;
    public Slider xCSlider;
    public Slider yCSlider;
    private void Awake()
    {
        Instance = this;
        xSlider.value = 30f;
        ySlider.value = 30f;
        xCSlider.value = 160f;
        yCSlider.value = 160f;
        cFOV.value = 60f;
        ChangeSensitivity();
    }
    public void ChangeSensitivity()
    {
        GameManager.Instance.GetSensitivity();
    }

}
