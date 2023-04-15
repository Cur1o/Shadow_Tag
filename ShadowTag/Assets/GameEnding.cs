using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScenesManager.Instance.EndGameSequence();
    }
}
