using UnityEngine;
using UnityEditor;
using Scripts.Editor.Interfaces;

namespace Scripts.Editor.MapSettings
{
    public class MapEditorSettings : IMapEditorSettings
    {
        private const string GRID_WIDTH_KEY = "MapGridWidth";
        private const string GRID_HEIGHT_KEY = "MapGridHeight";
        private const string GRID_SIZE_KEY = "MapGridSize";
        private const string GRID_OFFSET_KEY = "MapGridOffset";

        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public float GridSize { get; set; }
        public Vector3 GridOffset { get; set; }

        public void LoadSettings()
        {
            GridWidth = EditorPrefs.GetInt(GRID_WIDTH_KEY, 10);
            GridHeight = EditorPrefs.GetInt(GRID_HEIGHT_KEY, 10);
            GridSize = EditorPrefs.GetFloat(GRID_SIZE_KEY, 1.0f);
            GridOffset = JsonUtility.FromJson<Vector3>(EditorPrefs.GetString(GRID_OFFSET_KEY, JsonUtility.ToJson(Vector3.zero)));
        }

        public void SaveSettings()
        {
            EditorPrefs.SetInt(GRID_WIDTH_KEY, GridWidth);
            EditorPrefs.SetInt(GRID_HEIGHT_KEY, GridHeight);
            EditorPrefs.SetFloat(GRID_SIZE_KEY, GridSize);
            EditorPrefs.SetString(GRID_OFFSET_KEY, JsonUtility.ToJson(GridOffset));
        }
    }
}
