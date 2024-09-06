using UnityEngine;
using Scripts.Map;
using Scripts.Interfaces;
using System.Collections.Generic;

namespace Scripts.Turrets
{
    public class PlacementValidator : IPlacementValidator
    {
        public (bool canPlace, List<MapPiece> pieces) ValidatePlacement(List<Transform> rayTransforms)
        {
            var pieces = new List<MapPiece>();
            var allSlotsValid = true;

            foreach (Transform obj in rayTransforms)
            {
                if (Physics.Raycast(obj.position, Vector3.down, out RaycastHit hit))
                {
                    var placeSlot = hit.collider.GetComponent<IPlaceSlot>();

                    if (placeSlot == null || !placeSlot.CanPlace())
                    {
                        allSlotsValid = false;
                    }

                    var mapPiece = hit.transform.GetComponent<MapPiece>();
                    if (mapPiece != null)
                    {
                        pieces.Add(mapPiece);
                    }
                }
                else
                {
                    allSlotsValid = false;
                }
            }

            return (allSlotsValid, pieces);
        }
    }
}
