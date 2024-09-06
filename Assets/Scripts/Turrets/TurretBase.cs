using Scripts.Map;
using UnityEngine;
using Scripts.Interfaces;
using System.Collections.Generic;

namespace Scripts.Turrets
{
    public class TurretBase : MonoBehaviour, IPlaceable, IDragable, IInteractable
    {
        [SerializeField] private List<Transform> m_RayTransforms;

        private bool m_IsPlaced;
        private bool m_IsClicked;
        private bool m_IsDragged;
        private Vector3 m_ClickPosition;

        private IPlacementValidator m_PlacementValidator = new PlacementValidator();

        public void OnMouseDown() => Interact();

        public void OnMouseDrag() => Drag();

        public void OnMouseUp() => Place();

        public void Interact()
        {
            m_IsClicked = true;
            if (!m_IsPlaced)
            {
                m_ClickPosition = Input.mousePosition - GetScreenPosition();
            }
        }

        public void Drag()
        {
            if (m_IsPlaced || !m_IsClicked) return;

            var newPosition = Input.mousePosition - m_ClickPosition;
            if (m_ClickPosition != newPosition)
            {
                m_IsDragged = true;
                var worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
                worldPosition.z += worldPosition.y;
                worldPosition.y = transform.position.y;
                transform.position = worldPosition;

                CheckPlacement();
            }
        }

        public void Place()
        {
            if (m_IsPlaced) return;

            var (canPlace, pieces) = CheckPlacement();
            if (m_IsClicked && m_IsDragged && canPlace)
            {
                m_IsPlaced = true;
                if (pieces.Count == 4)
                {
                    SetTurretPosition(pieces);
                }
            }

            m_IsDragged = false;
            m_IsClicked = false;
        }

        private Vector3 GetScreenPosition() 
            => Camera.main.WorldToScreenPoint(transform.position);

        private (bool canPlace, List<MapPiece> pieces) CheckPlacement()
        {
            if (!m_IsDragged) return (false, default);

            return m_PlacementValidator.ValidatePlacement(m_RayTransforms);
        }

        private void SetTurretPosition(List<MapPiece> pieces)
        {
            float averageX = 0;
            float averageZ = 0;

            foreach (MapPiece piece in pieces)
            {
                piece.IsEmpty = false;
                piece.TargetObject = gameObject;
                averageX += piece.transform.position.x;
                averageZ += piece.transform.position.z;
            }

            averageX /= pieces.Count;
            averageZ /= pieces.Count;
            transform.position = new Vector3(averageX, 0, averageZ);
        }
    }
}
