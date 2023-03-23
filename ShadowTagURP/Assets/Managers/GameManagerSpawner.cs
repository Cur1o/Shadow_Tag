using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpawner : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    private void Awake(){if (!GameObject.FindGameObjectWithTag("GameManager"))Instantiate(gameManager);}
} 
