using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapPart
{
    public string id;
    public Texture2D thumbnail;
    public GameObject prefab;
    public int rotationY;
}

public class MapBuilder : MonoBehaviour
{
    public MapPart[] mapPartsList;
    public MapPart[]  currentMap;
    public Vector2Int mapSize;
    public float tileSize;
    [SerializeField] string mapName;

    [SerializeField] bool startOnAwake;

    //[SerializeField] bool randomMaze;
    public MapPart[] map;

    // Start is called before the first frame update
    void Start()
    {
        if(startOnAwake)
             Build();
    }
    //private void OnValidate()
    //{
    //    var tempmap = map;
    //    if (map == null || map.Length != mapSize.x * mapSize.y)
    //        map = new MapPart[mapSize.x * mapSize.y];
    //    for (int i = 0; i < tempmap.Length; i++)
    //    {
    //        map[i] = tempmap[i];
    //    }  
    //}
    public void Build()
    {          
        if (tileSize == 0f) return;

        var parent = new GameObject("Map_" + mapName);
        currentMap = map;

        for (int z = 0; z < mapSize.y; z++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                Vector3 position = new Vector3(x*tileSize, 0, z * tileSize);

                int index = x * mapSize.y + z;
                //Debug.Log(index);
                
                var prefab = map[index].prefab;

                if(prefab != null)
                {
                     var obj = Instantiate(prefab, position, Quaternion.Euler(0, map[index].rotationY, 0f));

                   
                    obj.transform.SetParent(parent.transform);
                   
                }              
            }
        }
    }
}
