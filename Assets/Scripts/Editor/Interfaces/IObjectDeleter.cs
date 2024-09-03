using UnityEngine;
using UnityEditor;

namespace Scripts.Editor.Interfaces
{
    public interface IObjectDeleter
    {
        public void DeleteObject(SceneView sceneView, Vector3 position);
    }
}
