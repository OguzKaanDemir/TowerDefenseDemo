using UnityEngine;

namespace Scripts.Interfaces
{
    public interface IPlaceSlot
    {
        public GameObject PlaceableObject { get; set; }
        public GameObject NotPlaceableObject { get; set; }
        public bool CanPlace();
        public bool Place();
    }
}
