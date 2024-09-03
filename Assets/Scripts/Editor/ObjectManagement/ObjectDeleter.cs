using UnityEngine;
using UnityEditor;
using Scripts.Editor.Interfaces;

namespace Scripts.Editor.ObjectManagement
{
    public class ObjectDeleter : IObjectDeleter
    {
        public void DeleteObject(SceneView sceneView, Vector3 position)
        {
            var colliders = Physics.OverlapBox(position + Vector3.one * 0.5f, Vector3.one * 0.05f);

            foreach (var collider in colliders)
            {
                if (collider.gameObject != null && collider.gameObject != sceneView.camera.gameObject)
                {
                    Undo.DestroyObjectImmediate(collider.gameObject);
                }
            }
        }
    }
}
