using UnityEngine;

namespace Scripts.Editor.Interfaces
{
    public interface IGridManager
    {
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public float GridSize { get; set; }
        public Vector3 GridOffset { get; set; }
        public Vector3 GetGridPosition(Vector3 hitPoint);
        public bool IsWithinGridBounds(Vector3 gridPosition);
        public void DrawGrid();
    }
}
