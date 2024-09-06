using UnityEngine;
using Scripts.Interfaces;
using Sirenix.OdinInspector;

namespace Scripts.Map
{
    public class MapPiece : MonoBehaviour, IMapPiece, IPlaceSlot
    {
        [field: SerializeField] public bool IsEmpty { get; set; }
        [field: SerializeField, HideIf(nameof(IsEmpty))] public GameObject TargetObject { get; set; }
        [field: SerializeField] public GameObject PlaceableObject { get; set; }
        [field: SerializeField] public GameObject NotPlaceableObject { get; set; }

        public bool CanPlace()
        {
            CancelInvoke(nameof(ResetObject));
            Invoke(nameof(ResetObject), 0.02f);

            if (IsEmpty)
            {
                PlaceableObject.SetActive(true);
                NotPlaceableObject.SetActive(false);
            }
            else
            {
                PlaceableObject.SetActive(false);
                NotPlaceableObject.SetActive(true);
            }
            return IsEmpty;
        }

        public bool Place()
        {
            return false;
        }

        private void ResetObject()
        {
            PlaceableObject.SetActive(false);
            NotPlaceableObject.SetActive(false);
        }
    }
}
