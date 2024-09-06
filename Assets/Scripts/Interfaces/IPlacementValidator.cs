using UnityEngine;
using Scripts.Map;
using System.Collections.Generic;

namespace Scripts.Interfaces
{
    public interface IPlacementValidator
    {
        (bool canPlace, List<MapPiece> pieces) ValidatePlacement(List<Transform> rayTransforms);
    }
}
