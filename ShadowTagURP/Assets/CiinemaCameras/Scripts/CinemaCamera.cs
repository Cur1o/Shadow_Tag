using System.Collections;
using UnityEngine;
public class CinemaCamera : MonoBehaviour
{
    private void Start()
    {
        ScenesManager.Instance.playerUI.SetActive(false); 
        StartCoroutine(DeactivateCamera());
    }
    private IEnumerator DeactivateCamera()
    {
        yield return new WaitForSeconds(25);
        ScenesManager.Instance.playerUI.SetActive(true);
        Destroy(gameObject);
    }
}
