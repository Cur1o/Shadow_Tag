using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Button backButton;
    private void Awake()
    {
        backButton.onClick.AddListener(deactivate);
    }
    private void deactivate()
    {
        SetWindowInactive(gameObject);
    }
    public void SetWindowInactive(GameObject obj)
    {
        Time.timeScale = 0;
        GameManager.Instance.inMenu = false;
        obj.SetActive(false);
    }
}
