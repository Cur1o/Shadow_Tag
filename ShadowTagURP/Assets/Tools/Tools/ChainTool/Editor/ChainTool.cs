using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ChainTool
{
    public enum Accuracy : int { Fast = 100, Good = 1000, VeryGood = 10000 }
    public class ChainTool : EditorWindow
    {
        [SerializeField] public static ChainTool Window;
        [SerializeField] GameObject prefab;

        [SerializeField] float width;
        [SerializeField] float gap;

        [SerializeField] Accuracy accuracyType;
        
        [SerializeField] Vector3[] points = new Vector3[2];
        [SerializeField] Vector3[] tangents = new Vector3[2];

        [SerializeField] bool pointsFoldout;

        const string saveName = "Chaintool_SaveData";

        private void OnEnable()
        {
            points[0] = new Vector3(0, 0, 0);
            points[1] = new Vector3(0, 0, 0);
            tangents[0] = new Vector3(0, 0, 0);
            tangents[1] = new Vector3(0, 0, 0);

            SceneView.duringSceneGui += OnSceneGUI;
         
            var data = EditorPrefs.GetString(saveName, JsonUtility.ToJson(this, false));
            JsonUtility.FromJsonOverwrite(data, this);
        }

        private void OnDisable()
        {
            var data = JsonUtility.ToJson(this, false);
            EditorPrefs.SetString(saveName, data);
        }

        [MenuItem("Window/Chain Tool")]
        static void Init()
        {
            Window = (ChainTool)EditorWindow.GetWindow(typeof(ChainTool));
            Window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("Select a prefab to spawn contiguously. " +
                "Prefabs with Rigidbody and a Joint Component (it doesn't matter which one) " +
                "are connected automatically.\n\nFor large bridges and chains, the mass of the individual" +
                " prefabs (in the Rigidbody) must be set very low.", MessageType.Info);

            prefab = (GameObject)EditorGUILayout.ObjectField(prefab, typeof(GameObject), false);

            width = EditorGUILayout.FloatField("Width", width);
            gap = EditorGUILayout.FloatField("Gap", gap);
            accuracyType = (Accuracy)EditorGUILayout.EnumPopup("Accuracy", accuracyType);

            EditorGUILayout.Space(10);

            pointsFoldout = EditorGUILayout.Foldout(pointsFoldout, "Point Vectors");

            if (pointsFoldout)
            {
                points[0] = EditorGUILayout.Vector3Field("Point 1", points[0]);
                points[1] = EditorGUILayout.Vector3Field("Point 2", points[1]);

                tangents[0] = EditorGUILayout.Vector3Field("Tangent 1", tangents[0]);
                tangents[1] = EditorGUILayout.Vector3Field("Tangent 2", tangents[1]);

            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Instantiate Objects"))
            {
                InstantiateObjects( gap, width,  1f/(float)accuracyType );
            }
        }

        void InstantiateObjects( float gap, float width, float accuracy)
        {
            var spawnPoints = Utils.CreateSpawnPoints(points[0], points[1], tangents[0], tangents[1], gap, width, accuracy);

            var parent = new GameObject("ChainTool_Parent");

            GameObject prevObj = null;

            for (int i = 0; i < spawnPoints.Count; i++)
            {
               var obj = PrefabUtility.InstantiatePrefab(prefab);

                if (obj != null)
                {
                    GameObject gO = obj as GameObject;
                    gO.transform.position = spawnPoints[i];
                    gO.transform.SetParent(parent.transform);

                    if(i < spawnPoints.Count - 1)
                    {
                        gO.transform.rotation = Quaternion.LookRotation(spawnPoints[i + 1] - spawnPoints[i], Vector3.up);
                        ConnectJoint3D(gO, prevObj);
                        ConnectJoint2D(gO, prevObj);


                    }
                    else if(i== spawnPoints.Count-1) //der letzte Punkt
                    {
                        gO.transform.rotation = Quaternion.LookRotation( spawnPoints[i] - spawnPoints[i - 1], Vector3.up);

                        ConnectJoint3D(gO, prevObj);
                        ConnectJoint2D(gO, prevObj);

                        if(gO.TryGetComponent( out Rigidbody rB))
                        {
                             gO.AddComponent<FixedJoint>();
                        }

                        if (gO.TryGetComponent(out Rigidbody2D rB2d))
                        {
                            gO.AddComponent<FixedJoint2D>();
                        }
                    }
                    prevObj = gO;
                }
            }          
        }

        void ConnectJoint3D( GameObject gO, GameObject prevObj)
        {
            if (gO.TryGetComponent(out Joint joint))
            {
                if (prevObj != null) //Vorgänger ist da
                {
                    if (prevObj.TryGetComponent(out Rigidbody rB))
                    {
                        joint.connectedBody = rB;
                    }
                }
            }
        }

        void ConnectJoint2D(GameObject gO, GameObject prevObj)
        {
            if (gO.TryGetComponent(out Joint2D joint))
            {
                if (prevObj != null) //Vorgänger ist da
                {
                    if (prevObj.TryGetComponent(out Rigidbody2D rB))
                    {
                        joint.connectedBody = rB;
                    }
                }
            }
        }

        void OnSceneGUI(SceneView sceneView)
        {
            Debug.Log(points.Length);
            points[0] = Handles.PositionHandle(points[0], Quaternion.identity);
            points[1] = Handles.PositionHandle(points[1], Quaternion.identity);

            tangents[0] = Handles.PositionHandle(tangents[0], Quaternion.identity);
            tangents[1] = Handles.PositionHandle(tangents[1], Quaternion.identity);

            Handles.DrawLine(points[0], tangents[0]);
            Handles.DrawLine(points[1], tangents[1]);

            Handles.DrawBezier(points[0], points[1], tangents[0], tangents[1], Color.yellow, Texture2D.whiteTexture, 2f);
        }
    }
}
