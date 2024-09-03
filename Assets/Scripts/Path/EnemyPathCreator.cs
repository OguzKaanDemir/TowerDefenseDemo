using Dreamteck.Splines;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Path
{
    public class EnemyPathCreator : MonoBehaviour
    {
        [SerializeField] private SplineComputer m_SplineComputer;
        [SerializeField] private List<Transform> m_PathPieces;

        [Button("Create Path")]
        private void CreatePath()
        {
            m_PathPieces.Clear();
            var startPiece = FindObjectsOfType<EnemyPathPiece>().FirstOrDefault(piece => piece.IsStartPiece);

            if (startPiece == null)
            {
                Debug.LogError("Baþlangýç parçasý bulunamadý!");
                return;
            }

            if (!FindPath(startPiece.transform))
            {
                Debug.LogError("Finish parçasýna ulaþýlamadý veya yol bulunamadý!");
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
                Debug.Log("Bitiþ parçasýna ulaþýldý!");
                return true;
            }

            Vector3[] directions = { Vector3.right, Vector3.left, Vector3.forward, Vector3.back};

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
                Debug.LogError("Spline oluþturmak için yeterli yol parçasý yok!");
                return;
            }

            var points = new SplinePoint[m_PathPieces.Count];

            for (int i = 0; i < m_PathPieces.Count; i++)
            {
                points[i] = new SplinePoint
                {
                    position = m_PathPieces[i].position,
                };
            }

            m_SplineComputer.SetPoints(points);
            Debug.Log("Spline baþarýyla oluþturuldu!");
        }
    }
}
