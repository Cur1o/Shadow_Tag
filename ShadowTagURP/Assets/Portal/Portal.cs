using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SaveManager.Instance.gameData.currentLabyrinthLevel++;
        SaveManager.Instance.gameData.playerPosition = Vector3.zero;
        ScenesManager.Instance.LoadNextScene();
    }

}
