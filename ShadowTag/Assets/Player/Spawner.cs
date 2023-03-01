using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Vector3 position;
    // Start is called before the first frame update
    void Start()
    {

        position = transform.position;
        Spawn(prefab,position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Spawn(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab,position,Quaternion.identity);
    }
}
