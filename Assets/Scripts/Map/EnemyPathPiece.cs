using UnityEngine;
using Scripts.Interfaces;
using Sirenix.OdinInspector;

namespace Scripts.Map
{
    public class EnemyPathPiece : MapPiece, IEnemyPathPiece
    {
        [field: SerializeField, HideIf(nameof(IsEndPiece))] public bool IsStartPiece { get; set; }
        [field: SerializeField, HideIf(nameof(IsStartPiece))] public bool IsEndPiece { get; set; }
    }
}
