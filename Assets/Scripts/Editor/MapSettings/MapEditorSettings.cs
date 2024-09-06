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
            GridWidth = PlayerPrefs.GetInt(GRID_WIDTH_KEY, 10);
            GridHeight = PlayerPrefs.GetInt(GRID_HEIGHT_KEY, 10);
            GridSize = PlayerPrefs.GetFloat(GRID_SIZE_KEY, 1.0f);
            GridOffset = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(GRID_OFFSET_KEY, JsonUtility.ToJson(Vector3.zero)));
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt(GRID_WIDTH_KEY, GridWidth);
            PlayerPrefs.SetInt(GRID_HEIGHT_KEY, GridHeight);
            PlayerPrefs.SetFloat(GRID_SIZE_KEY, GridSize);
            PlayerPrefs.SetString(GRID_OFFSET_KEY, JsonUtility.ToJson(GridOffset));
        }
    }
}
