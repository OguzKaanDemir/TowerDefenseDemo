using UnityEngine;

namespace Scripts.Interfaces
{
    public interface IMapPiece
    {
        public bool IsEmpty { get; set; }
        public GameObject TargetObject { get; set; }
    }
}
