using UnityEngine;
using Scripts.Interfaces;
using Sirenix.OdinInspector;

namespace Scripts.Path
{
    public class MapPiece : MonoBehaviour, IMapPiece
    {
        [field: SerializeField] public bool IsEmpty { get; set; }
        [field: SerializeField, HideIf(nameof(IsEmpty))] public GameObject TargetObject { get; set; }
    }
}
