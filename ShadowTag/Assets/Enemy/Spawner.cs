using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace spawner
{
    public class Spawner : MonoBehaviour
    {
        [Header("Game Object")]
        [SerializeField] private GameObject prefab;
        private Vector3 position;
        [Header("Special Cases")]
        [SerializeField] private float spawnTimer = 0.5f;
        [SerializeField] private bool isGhostStation;
        [SerializeField] private bool isHub;
        [SerializeField] private bool isPlayer;
        [SerializeField] private bool isWeaponDisplay;
        private bool isFirst = true;
        Vector3 loadedPosition;
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
            else if (isWeaponDisplay)
                Instantiate(prefab, position, prefab.transform.rotation , transform);
            else if(!isGhostStation)
                Instantiate(prefab, position, transform.rotation,transform);
            
        }
        private IEnumerator SpawnEnumerator(GameObject prefab, Vector3 position)
        {
            yield return new WaitForSeconds(spawnTimer);
            if (loadedPosition != Vector3.zero && !isHub)
                Instantiate(prefab, loadedPosition + new Vector3(0, 0.25f, 0) , Quaternion.identity);
            else
                Instantiate(prefab, position, Quaternion.identity);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (isFirst)
            {
                isFirst = false;
                Instantiate(prefab, position, transform.rotation);
            }
            
        }
    }
}
