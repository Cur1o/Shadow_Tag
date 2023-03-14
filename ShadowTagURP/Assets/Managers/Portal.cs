using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SaveManager.Instance.gameData.currentLabyrinthLevel++;
        ScenesManager.Instance.LoadNextScene();
    }

}
