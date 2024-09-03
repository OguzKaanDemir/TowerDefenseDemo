using UnityEngine;
using UnityEditor;
using Scripts.Editor.Grid;
using Scripts.Editor.Interfaces;
using Scripts.Editor.MapSettings;
using Scripts.Editor.ObjectManagement;

namespace Scripts.Editor.MapCreator
{
    public class CreateMapWindow : EditorWindow
    {
        private IGridManager m_GridManager;
        private IObjectSpawner m_ObjectSpawner;
        private IObjectDeleter m_ObjectDeleter;
        private IMapEditorSettings m_EditorSettings;

        private GameObject[] m_MapPrefabs;
        private GameObject m_SelectedPrefab;
        private Vector2 m_ScrollPos;
        private Vector3 m_LastGridPosition = Vector3.one * 99999;

        [MenuItem("Tools/Create Map")]
        public static void ShowWindow()
        {
            GetWindow<CreateMapWindow>("Map Creator");
        }

        private void OnEnable()
        {
            m_MapPrefabs = Resources.LoadAll<GameObject>("Map Blocks");

            m_EditorSettings = new MapEditorSettings();
            m_EditorSettings.LoadSettings();

            m_GridManager = new GridManager(m_EditorSettings);
            m_ObjectSpawner = new ObjectSpawner();
            m_ObjectDeleter = new ObjectDeleter();

            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
            m_EditorSettings.SaveSettings();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Grid Settings", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            m_GridManager.GridWidth = EditorGUILayout.IntField("Grid Width", m_GridManager.GridWidth);
            m_GridManager.GridHeight = EditorGUILayout.IntField("Grid Height", m_GridManager.GridHeight);
            m_GridManager.GridSize = EditorGUILayout.FloatField("Grid Size", m_GridManager.GridSize);
            m_GridManager.GridOffset = EditorGUILayout.Vector3Field("Grid Offset", m_GridManager.GridOffset);

            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Map Blocks", EditorStyles.boldLabel);
            m_ScrollPos = EditorGUILayout.BeginScrollView(m_ScrollPos);

            int columns = 4;
            for (int i = 0; i < m_MapPrefabs.Length; i++)
            {
                if (i % columns == 0) EditorGUILayout.BeginHorizontal();

                Texture2D preview = AssetPreview.GetAssetPreview(m_MapPrefabs[i]);
                if (GUILayout.Button(preview, GUILayout.Width(64), GUILayout.Height(64)))
                {
                    m_SelectedPrefab = m_MapPrefabs[i];
                }

                if (i % columns == columns - 1 || i == m_MapPrefabs.Length - 1)
                {
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Selected Prefab", EditorStyles.boldLabel);

            if (m_SelectedPrefab != null)
            {
                GUILayout.Label("Name: " + m_SelectedPrefab.name);
                Texture2D selectedPreview = AssetPreview.GetAssetPreview(m_SelectedPrefab);
                GUILayout.Label(selectedPreview, GUILayout.Width(64), GUILayout.Height(64));
            }
            else
            {
                GUILayout.Label("No prefab selected");
            }
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            if (!sceneView.hasFocus) return;

            m_GridManager.DrawGrid();

            Event e = Event.current;
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);

            SceneView.RepaintAll();

            Plane plane = new Plane(Vector3.up, m_GridManager.GridOffset);
            if (plane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 gridPosition = m_GridManager.GetGridPosition(hitPoint);

                if (m_GridManager.IsWithinGridBounds(gridPosition))
                {
                    if (e.type == EventType.MouseDown)
                    {
                        if (e.button == 0)
                        {
                            m_ObjectSpawner.SpawnObject(sceneView, m_ObjectDeleter, m_SelectedPrefab, gridPosition);
                        }
                        else if (e.button == 1)
                        {
                            m_ObjectDeleter.DeleteObject(sceneView, gridPosition);
                        }

                        e.Use();
                    }
                    else if (e.type == EventType.MouseUp && e.button == 0)
                    {
                        e.Use();
                        m_LastGridPosition = Vector3.one * 99999;
                    }

                    if (e.type == EventType.MouseDrag)
                    {
                        if (e.button == 1)
                        {
                            m_ObjectDeleter.DeleteObject(sceneView, gridPosition);
                        }
                        else if (e.button == 0 && m_SelectedPrefab != null)
                        {
                            if (m_LastGridPosition != gridPosition + Vector3.one * 0.5f)
                            {
                                m_LastGridPosition = gridPosition + Vector3.one * 0.5f;
                                m_ObjectSpawner.SpawnObject(sceneView, m_ObjectDeleter, m_SelectedPrefab, gridPosition);
                            }
                        }

                        e.Use();
                    }

                    Handles.color = Color.red;
                    Handles.DrawWireCube(gridPosition + Vector3.one * 0.5f, Vector3.one * m_GridManager.GridSize);

                    Handles.color = Color.white;
                    Handles.DrawWireCube(gridPosition + Vector3.one * 0.5f, Vector3.one * m_GridManager.GridSize * 0.05f);
                }
            }
        }
    }
}
