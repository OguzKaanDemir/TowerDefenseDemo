using UnityEngine;
using Scripts.Turrets;
using System.Collections.Generic;

namespace Scripts.Interfaces
{
    public interface ITurret
    {
        public List<Transform> RayTransforms { get; set; }
        public DistanceCircleController DistanceCircle { get; set; }
        public float DistanceCircleRadius { get; set; }
    }
}
