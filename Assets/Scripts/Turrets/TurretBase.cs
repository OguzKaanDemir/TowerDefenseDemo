using Scripts.Map;
using UnityEngine;
using Scripts.Interfaces;
using System.Collections.Generic;

namespace Scripts.Turrets
{
    public class TurretBase : MonoBehaviour, ITurret, IPlaceable, IDragable, IInteractable
    {
        [field: SerializeField] public List<MeshRenderer> RenderersToChange { get; set; }
        [field: SerializeField] public ParticleSystem PlaceParticle { get; set; }
        [field: SerializeField] public bool IsPlaced { get; set; }
        [field: SerializeField] public bool IsClicked { get; set; }
        [field: SerializeField] public bool IsDragged { get; set; }
        [field: SerializeField] public List<Transform> RayTransforms { get; set; }
        [field: SerializeField] public DistanceCircleController DistanceCircle { get; set; }
        [field: SerializeField] public float DistanceCircleRadius { get; set; }

        private Vector3 m_ClickPosition;
        private IPlacementValidator m_PlacementValidator = new PlacementValidator();


        private void Start()
        {
            DistanceCircle.SetRadius(DistanceCircleRadius);
        }

        public void OnMouseDown() => Interact();

        public void OnMouseDrag() => Drag();

        public void OnMouseUp() => Place();

        public void Interact()
        {
            IsClicked = true;
            if (!IsPlaced)
            {
                m_ClickPosition = Input.mousePosition - GetScreenPosition();
            }
        }

        public void Drag()
        {
            if (IsPlaced || !IsClicked) return;

            var newPosition = Input.mousePosition - m_ClickPosition;
            if (m_ClickPosition != newPosition)
            {
                IsDragged = true;
                var worldPosition = Camera.main.ScreenToWorldPoint(newPosition);
                worldPosition.z += worldPosition.y;
                worldPosition.y = transform.position.y;
                transform.position = worldPosition;

                DistanceCircle.SetActive(true);
                DistanceCircle.SetCircleColor(CheckPlacement().canPlace);
            }
        }

        public void Place()
        {
            if (IsPlaced) return;

            var (canPlace, pieces) = CheckPlacement();
            if (IsClicked && IsDragged && canPlace)
            {
                IsPlaced = true;
                if (pieces.Count == 4)
                {
                    SetTurretPosition(pieces);
                    PlaceParticle.Play();
                    foreach (var renderer in RenderersToChange)
                    {
                        renderer.material.SetFloat("_Cutoff", 0f);
                    }
                    DistanceCircle.SetActive(false);
                }
            }
            else
            {
                Destroy(gameObject);
            }

            IsDragged = false;
            IsClicked = false;
        }

        private Vector3 GetScreenPosition()
            => Camera.main.WorldToScreenPoint(transform.position);

        private (bool canPlace, List<MapPiece> pieces) CheckPlacement()
        {
            if (!IsDragged) return (false, default);

            return m_PlacementValidator.ValidatePlacement(RayTransforms);
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
