using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpawner : MonoBehaviour
{
    [SerializeField] GameObject gameManager;    //GameManger Prefab
    private void Awake(){if (!GameObject.FindGameObjectWithTag("GameManager"))Instantiate(gameManager);}    //If  GameManager doues not exist, a new one will be instanciated.
} 
