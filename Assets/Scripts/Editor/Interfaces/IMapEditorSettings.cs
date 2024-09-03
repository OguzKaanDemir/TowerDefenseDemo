using UnityEngine;

namespace Scripts.Editor.Interfaces
{
    public interface IMapEditorSettings
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public float GridSize { get; set; }
        public Vector3 GridOffset { get; set; }
        public void LoadSettings();
        public void SaveSettings();
    }
}
