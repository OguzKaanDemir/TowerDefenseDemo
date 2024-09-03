using UnityEngine;
using UnityEditor;

namespace Scripts.Editor.Interfaces
{
    public interface IObjectSpawner
    {
        public void SpawnObject(SceneView sceneView, IObjectDeleter objectDeleter, GameObject prefab, Vector3 position);
    }
}
