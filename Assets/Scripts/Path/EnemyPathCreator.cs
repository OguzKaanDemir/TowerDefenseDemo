using Scripts.Map;
using System.Linq;
using UnityEngine;
using Dreamteck.Splines;
using System.Collections.Generic;

namespace Scripts.Path
{
    public class EnemyPathCreator : MonoBehaviour
    {
        [SerializeField] private SplineComputer m_SplineComputer;
        [SerializeField] private List<Transform> m_PathPieces;

        public void CreatePath()
        {
            m_SplineComputer.transform.position = Vector3.zero;
            m_PathPieces.Clear();
            var startPiece = FindObjectsOfType<EnemyPathPiece>().FirstOrDefault(piece => piece.IsStartPiece);

            if (startPiece == null)
            {
                Debug.LogError("Starter piece not found");
                return;
            }

            if (!FindPath(startPiece.transform))
            {
                Debug.LogError("Finish part not found or path not found");
                return;
            }

            CreateSplineFromPath();
        }

        private bool FindPath(Transform currentPiece)
        {
            if (m_PathPieces.Contains(currentPiece)) return false;

            m_PathPieces.Add(currentPiece);

            var pieceComponent = currentPiece.GetComponent<EnemyPathPiece>();
            if (pieceComponent.IsEndPiece)
            {
                Debug.Log("End piece reached");
                return true;
            }

            Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

            foreach (var direction in directions)
            {
                Debug.DrawRay(currentPiece.position, direction * 1f, Color.yellow, 1f);

                if (Physics.Raycast(currentPiece.position, direction, out RaycastHit hit, 1f))
                {
                    var nextPiece = hit.collider.GetComponent<EnemyPathPiece>();

                    if (nextPiece != null && FindPath(nextPiece.transform))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void CreateSplineFromPath()
        {
            if (m_PathPieces == null || m_PathPieces.Count < 2)
            {
                Debug.LogError("Not enough pieces to create spline");
                return;
            }

            var points = new SplinePoint[m_PathPieces.Count];

            for (int i = 0; i < m_PathPieces.Count; i++)
            {
                var pos = m_PathPieces[i].position;

                points[i] = new SplinePoint
                {
                    position = pos
                };
            }

            m_SplineComputer.SetPoints(points);
            m_SplineComputer.transform.position = Vector3.up * 0.5f;
            Debug.Log("Spline created successfully!");
        }
    }
}
