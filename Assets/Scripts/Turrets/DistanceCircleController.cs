using UnityEngine;

namespace Scripts.Turrets
{
    public class DistanceCircleController : MonoBehaviour
    {
        [SerializeField] private MeshRenderer m_Renderer;

        public void SetRadius(float value)
        {
            transform.localScale = Vector3.one * value;
        }

        public void SetCircleColor(bool canPlace)
        {
            m_Renderer.material.SetColor("_TintColor", canPlace ? Color.green : Color.red);
        }

        public void SetActive(bool isActive)
        {
            m_Renderer.enabled = isActive;
        }
    }
}
