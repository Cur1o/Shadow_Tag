using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public Button backButton;
    [SerializeField] Scrollbar scrollVertical;
    
    private void Awake()
    {
        backButton.onClick.AddListener(deactivate);
    }
    public void StartAnimation()
    {
        scrollVertical.value = 1f;
        StartCoroutine(AnimationCorutine());
    }
    private IEnumerator AnimationCorutine()
    {
        yield return new WaitForSeconds(0.2f);
        while (scrollVertical.value >= 0.2f)
        {
            yield return new WaitForSeconds(0.1f);
            scrollVertical.value -= 0.001f;
        }
    }
    private void deactivate()
    {
        SetWindowInactive(gameObject);
    }
    public void SetWindowInactive(GameObject obj)
    {
        Time.timeScale = 0;
        obj.SetActive(false);
    }
    
}
