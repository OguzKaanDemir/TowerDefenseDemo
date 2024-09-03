using UnityEngine;
using UnityEditor;
using Scripts.Editor.Interfaces;

namespace Scripts.Editor.Grid
{
    public class GridManager : IGridManager
    {
        private IMapEditorSettings m_Settings;

        public GridManager(IMapEditorSettings settings)
        {
            m_Settings = settings;
        }

        public int GridWidth
        {
            get => m_Settings.GridWidth;
            set => m_Settings.GridWidth = value;
        }

        public int GridHeight
        {
            get => m_Settings.GridHeight;
            set => m_Settings.GridHeight = value;
        }

        public float GridSize
        {
            get => m_Settings.GridSize;
            set => m_Settings.GridSize = value;
        }

        public Vector3 GridOffset
        {
            get => m_Settings.GridOffset;
            set => m_Settings.GridOffset = value;
        }

        public Vector3 GetGridPosition(Vector3 hitPoint)
        {
            return new Vector3(
                Mathf.Floor((hitPoint.x - GridOffset.x) / GridSize) * GridSize + GridOffset.x,
                GridOffset.y,
                Mathf.Floor((hitPoint.z - GridOffset.z) / GridSize) * GridSize + GridOffset.z
            );
        }

        public bool IsWithinGridBounds(Vector3 gridPosition)
        {
            return gridPosition.x >= GridOffset.x && gridPosition.x < GridWidth * GridSize + GridOffset.x &&
                   gridPosition.z >= GridOffset.z && gridPosition.z < GridHeight * GridSize + GridOffset.z;
        }

        public void DrawGrid()
        {
            Handles.color = Color.green;
            for (int x = 0; x <= GridWidth; x++)
            {
                Handles.DrawLine(
                    new Vector3(x * GridSize + GridOffset.x, GridOffset.y, GridOffset.z),
                    new Vector3(x * GridSize + GridOffset.x, GridOffset.y, GridHeight * GridSize + GridOffset.z)
                );
            }

            for (int z = 0; z <= GridHeight; z++)
            {
                Handles.DrawLine(
                    new Vector3(GridOffset.x, GridOffset.y, z * GridSize + GridOffset.z),
                    new Vector3(GridWidth * GridSize + GridOffset.x, GridOffset.y, z * GridSize + GridOffset.z)
                );
            }
        }
    }
}
