using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MapTool
{

[System.Serializable]
public class MapBuilderTool : EditorWindow
{
    static MapBuilderTool Window;

    [SerializeField] Vector2 mapScroll;
    [SerializeField] Vector2 toolbarScroll;
    [SerializeField] int buttonSizeMap = 50;
    [SerializeField] int buttonSizeTool = 80;

    const string saveName = "MapTool_SaveData";
    int mapPartSlot;

    MapBuilder mapBuilder = null;

    #region Load/Save Settings
    private void OnEnable()
    {        
         var data = EditorPrefs.GetString(saveName, JsonUtility.ToJson(this, false));
         JsonUtility.FromJsonOverwrite(data, this);
    }

    private void OnDisable()
    {
         var data = JsonUtility.ToJson(this, false);
         EditorPrefs.SetString(saveName, data);
    }
    #endregion

    #region Init and Update
    [MenuItem("Window/Map Builder Tool")]
    static void Init()
    {
        Window = (MapBuilderTool)EditorWindow.GetWindow(typeof(MapBuilderTool), false, "Map Builder" );
        Window.Show();
    }

    private void OnSelectionChange()
    {
        if(Selection.activeGameObject != null 
                && Selection.activeGameObject.TryGetComponent( out MapBuilder mapBuilder))
        {
                this.mapBuilder = mapBuilder;
        }   
    }

    #endregion

    #region Tool GUI

    private void OnGUI()
    {
        if (mapBuilder == null) return;

      
       buttonSizeTool = EditorGUILayout.IntSlider("Tool Button Size", buttonSizeTool, 20, 80);
       buttonSizeMap = EditorGUILayout.IntSlider("Map Button Size", buttonSizeMap, 20, 80);
    
       SlotToolbarGUI();
       mapScroll = EditorGUILayout.BeginScrollView(mapScroll);
           MapGUI();
       EditorGUILayout.EndScrollView();
       GUILayout.FlexibleSpace();
    }

    void SlotToolbarGUI()
    {
        GUIContent[] toolbarContent = new GUIContent[mapBuilder.mapPartsList.Length+1];

        for (int i = 0; i < mapBuilder.mapPartsList.Length; i++)
        {
            Texture2D thumbnail = mapBuilder.mapPartsList[i].thumbnail;

            if (thumbnail == null)
                thumbnail = AssetPreview.GetAssetPreview(mapBuilder.mapPartsList[i].prefab);

            toolbarContent[i] = new GUIContent( thumbnail,mapBuilder.mapPartsList[i].id);        
        }

        toolbarContent[mapBuilder.mapPartsList.Length] = new GUIContent("x");

        toolbarScroll = EditorGUILayout.BeginScrollView(toolbarScroll);
             mapPartSlot = GUILayout.Toolbar(mapPartSlot, toolbarContent, MapHelper.GetToolbarStyle(buttonSizeTool), GUILayout.MaxHeight(buttonSizeTool+5)); 
       EditorGUILayout.EndScrollView();
     }

    void MapGUI()
    {
            for (int y = 0; y < mapBuilder.mapSize.y; y++)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < mapBuilder.mapSize.x; x++)
                {
                    int index = y * mapBuilder.mapSize.x + x;

                    string id = mapBuilder.map[index].id;

                    Texture2D thumbnail = mapBuilder.map[index].thumbnail;
                    if (thumbnail == null)
                    {
                        Object preview = mapBuilder.map[index].prefab;
                        thumbnail = AssetPreview.GetAssetPreview(preview);
                    }

                    GUIContent buttonContent = new GUIContent(thumbnail);
                  
                    if (GUILayout.Button(buttonContent, MapHelper.GetToolbarStyle(buttonSizeMap)))
                    {
                        SetMapField(index);
                    }
                }
                GUILayout.EndHorizontal();
            }
    }

    void SetMapField(int index)
    {
        if (mapPartSlot == mapBuilder.mapPartsList.Length)
        {
            mapBuilder.map[index].id = "";
            mapBuilder.map[index].prefab = null;
            mapBuilder.map[index].thumbnail = null;
            mapBuilder.map[index].rotationY = 0;
        }
        else
        {
            mapBuilder.map[index].id = mapBuilder.mapPartsList[mapPartSlot].id;
            mapBuilder.map[index].prefab = mapBuilder.mapPartsList[mapPartSlot].prefab;

            if (mapBuilder.map[index].thumbnail == null)
                mapBuilder.map[index].thumbnail = mapBuilder.mapPartsList[mapPartSlot].thumbnail;

            mapBuilder.map[index].rotationY += 90;
            mapBuilder.map[index].rotationY = mapBuilder.map[index].rotationY % 360;

            var tex = MapHelper.RotateTexture(mapBuilder.map[index].thumbnail, true);

            mapBuilder.map[index].thumbnail = tex;
        }
    }

    #endregion
    }
}

