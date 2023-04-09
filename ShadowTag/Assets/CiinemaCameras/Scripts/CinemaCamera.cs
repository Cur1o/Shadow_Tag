using System.Collections;
using UnityEngine;
public class CinemaCamera : MonoBehaviour
{
    private void Start()
    {
        ScenesManager.Instance.playerUI.SetActive(false); 
        //StartCoroutine(DeactivateCamera());
    }
    public void DeactivateCamera()
    {
        GameManager.Instance.SkipIntro();
    }
}
