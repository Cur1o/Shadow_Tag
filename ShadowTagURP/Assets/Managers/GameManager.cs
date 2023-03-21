using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float xSliderValue;
    public float ySliderValue;
    public float xCSliderValue;
    public float yCSliderValue;
    public int cFOV;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        GetSensitivity();
    }
    public void GetSensitivity()
    {
        cFOV = (int)Settings.Instance.cFOV.value;
        xSliderValue = Settings.Instance.xSlider.value;
        ySliderValue = Settings.Instance.ySlider.value;
        xCSliderValue = Settings.Instance.xCSlider.value;
        yCSliderValue = Settings.Instance.yCSlider.value;
    }
}
