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
        [SerializeField] Vector3 loadedPosition;
        // Start is called before the first frame update
        void Start()
        {
            position = transform.position;
            loadedPosition = SaveManager.Instance.gameData.playerPosition;
            Spawn(prefab, position);
        }
        private void Spawn(GameObject prefab, Vector3 position)
        {
            if (isPlayer)
                StartCoroutine(SpawnEnumerator(prefab,position));
            else
                Instantiate(prefab, position, transform.rotation);
        }
        private IEnumerator SpawnEnumerator(GameObject prefab, Vector3 position)
        {
            yield return new WaitForSeconds(spawnTimer);
            if (loadedPosition != Vector3.zero)
                Instantiate(prefab, loadedPosition + new Vector3(0, 0.25f, 0) , Quaternion.identity);
            else
                Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
