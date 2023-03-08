using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace spawner
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        private Vector3 position;
        [SerializeField] private float spawnTimer = 0.5f;
        [SerializeField] private bool isPlayer;
        // Start is called before the first frame update
        void Start()
        {
            position = transform.position;
            Spawn(prefab, position);
        }
        private void Spawn(GameObject prefab, Vector3 position)
        {
            if (isPlayer)
                StartCoroutine(SpawnEnumerator(prefab,position));
            else
                Instantiate(prefab, position, Quaternion.identity);
        }
        private IEnumerator SpawnEnumerator(GameObject prefab, Vector3 position)
        {
            yield return new WaitForSeconds(spawnTimer);
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
