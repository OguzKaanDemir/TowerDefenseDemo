using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Interfaces
{
    public interface IPlaceable
    {
        public List<MeshRenderer> RenderersToChange { get; set; }
        public ParticleSystem PlaceParticle { get; set; }
        public void Place();
    }
}
